namespace SmartBusAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TripController : BaseController
    {
        private readonly IBusRepository busRepository;
        private readonly IParentRepository parentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly IPushNotificationService pushNotificationService;

        public TripController(IBusRepository busRepository,
                              IParentRepository parentRepository,
                              IStudentRepository studentRepository,
                              INotificationRepository notificationRepository,
                              IPushNotificationService pushNotificationService)
        {
            this.busRepository = busRepository;
            this.parentRepository = parentRepository;
            this.studentRepository = studentRepository;
            this.notificationRepository = notificationRepository;
            this.pushNotificationService = pushNotificationService;
        }

        [HttpPost("start-bus-trip")]
        public async Task<IActionResult> StartBusTrip(StartTripDto startTripDto)
        {
            ErrorOr<string> result;
            Bus bus = await busRepository.GetBusById(startTripDto.BusID);
            if (bus == null)
            {
                result = Error.NotFound("StartBusTrip", string.Format("No bus is found with the given ID({0}).", startTripDto.BusID));
            }
            else
            {
                if (bus.IsInService)
                {
                    result = Error.Failure("StopBusTrip", "Cannot start the bus because its already in service.");
                }
                else
                {
                    bus.IsInService = true;
                    bus.CurrentLocation = "";
                    bus.DestinationType = startTripDto.DestinationType;
                    bus.Students ??= new List<Student>();
                    bus.Notifications ??= new List<Notification>();
                    await busRepository.UpdateBus(bus);
                    result = string.Format("Successfully started the bus with the given ID({0}).", bus.ID);
                }
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost("stop-bus-trip")]
        public async Task<IActionResult> StopBusTrip(StopTripDto stopTripDto)
        {
            ErrorOr<string> result;
            Bus bus = await busRepository.GetBusById(stopTripDto.BusID);
            if (bus == null)
            {
                result = Error.NotFound("StopBusTrip", string.Format("No bus is found with the given ID({0}).", stopTripDto.BusID));
            }
            else
            {
                if (!bus.IsInService)
                {
                    result = Error.Failure("StopBusTrip", "Cannot stop the bus because its already not in service.");
                }
                else
                {
                    bus.IsInService = false;
                    bus.CurrentLocation = "";
                    bus.DestinationType = DestinationType.None;
                    bus.Students.Clear();
                    await busRepository.UpdateBus(bus);
                    result = string.Format("Successfully stopped the bus with the given ID({0}).", bus.ID);
                }
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost("update-bus-location")]
        public async Task<IActionResult> UpdateBusLocation(UpdateBusLocationDto updateBusLocationDto)
        {
            ErrorOr<string> result;
            Bus bus = await busRepository.GetBusById(updateBusLocationDto.BusID);

            if (bus == null)
            {
                result = Error.NotFound("UpdateBusLocation", string.Format("No bus is found with the given ID({0}).", updateBusLocationDto.BusID));
            }
            else
            {
                if (!bus.IsInService)
                {
                    result = Error.Failure("UpdateBusLocation", "Cannot update the bus location, since bus is not in service.");
                }
                else
                {
                    bus.CurrentLocation = updateBusLocationDto.CurrentLocation;
                    await busRepository.UpdateBus(bus);
                    result = string.Format("The Bus with the given ID({0}) its current location is updated to: {1}.", bus.ID, bus.CurrentLocation);
                }
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost("update-student-status-on-bus")]
        public async Task<IActionResult> UpdateStudentStatusOnBus(UpdateStudentStatusOnBusDto updateStudentStatusOnBusDto)
        {
            ErrorOr<string> result = "Successfully set the student on board.";
            Student student = await studentRepository.GetStudentById(updateStudentStatusOnBusDto.StudentID);
            Bus bus = await busRepository.GetBusById(updateStudentStatusOnBusDto.BusID);

            if (student == null)
            {
                result = Error.NotFound("UpdateStudentStatusOnBus", string.Format("No student is found with the given ID({0}).", updateStudentStatusOnBusDto.StudentID));
            }
            else if (bus == null)
            {
                result = Error.NotFound("UpdateStudentStatusOnBus", string.Format("No bus is found with the given ID({0}).", updateStudentStatusOnBusDto.BusID));
            }
            else if (bus.DestinationType == DestinationType.None || !bus.IsInService)
            {
                result = Error.Conflict("UpdateStudentStatusOffBus", "The bus should be in service and have a valid destination point to update the current student place.");
            }
            else
            {
                student.LastSeen = updateStudentStatusOnBusDto.Timestamp;
                student.IsAtSchool = false;
                student.IsAtHome = false;
                student.IsOnBus = true;
                student.BusID = updateStudentStatusOnBusDto.BusID;
                await studentRepository.UpdateStudent(student);

                bus.Students.Add(student);
                await busRepository.UpdateBus(bus);

                Notification notification = new()
                {
                    Title = "Student Boarded Notification",
                    Timestamp = DateTime.Now,
                    ParentID = student.ParentID,
                    Message = $"Student {student.FirstName} {student.LastName} boarded the bus at {updateStudentStatusOnBusDto.Timestamp}."
                };
                await notificationRepository.AddNotification(notification);

                await pushNotificationService.SendNotification(notification.Title, 
                                                               notification.Message, 
                                                               new PushNotificationConsts.LiveMapPushData(), 
                                                               student.ParentID, 
                                                               null);
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost("update-student-status-off-bus")]
        public async Task<IActionResult> UpdateStudentStatusOffBus(UpdateStudentStatusOffBusDto updateStudentStatusOffBusDto)
        {
            ErrorOr<string> result = "Successfully set the student off the bus.";
            Student student = await studentRepository.GetStudentById(updateStudentStatusOffBusDto.StudentID);
            Bus bus = await busRepository.GetBusById(updateStudentStatusOffBusDto.BusID);

            if (student == null)
            {
                result = Error.NotFound("UpdateStudentStatusOffBus", string.Format("No student is found with the given ID({0}).", updateStudentStatusOffBusDto.StudentID));
            }
            else if (bus == null)
            {
                result = Error.NotFound("UpdateStudentStatusOffBus", string.Format("No bus is found with the given ID({0}).", updateStudentStatusOffBusDto.BusID));
            }
            else
            {
                Dictionary<DestinationType, Func<string>> statusUpdateFunctions = new()
                {
                    {
                        DestinationType.Home, () =>
                        {
                            student.LastSeen = updateStudentStatusOffBusDto.Timestamp;
                            student.IsAtSchool = false;
                            student.IsAtHome = true;
                            student.IsOnBus = false;
                            student.BusID = null;
                            return $"Student {student.FirstName} {student.LastName} was dropped off at home at {updateStudentStatusOffBusDto.Timestamp}.";
                        }
                    },
                    {
                        DestinationType.School, () =>
                        {
                            student.LastSeen = updateStudentStatusOffBusDto.Timestamp;
                            student.IsAtSchool = true;
                            student.IsAtHome = false;
                            student.IsOnBus = false;
                            student.BusID = null;
                            return $"Student {student.FirstName} {student.LastName} arrived at school at {DateTime.Now}.";
                        }
                    }
                };

                if (statusUpdateFunctions.TryGetValue(bus.DestinationType, out Func<string> statusUpdateFunction))
                {
                    await studentRepository.UpdateStudent(student);
                    bus.Students.Remove(student);
                    await busRepository.UpdateBus(bus);

                    Notification notification = new()
                    {
                        Title = "Student Arrival Notification",
                        Timestamp = DateTime.Now,
                        ParentID = student.ParentID,
                        Message = statusUpdateFunction()
                    };
                    await notificationRepository.AddNotification(notification);

                    await pushNotificationService.SendNotification(notification.Title,
                                               notification.Message,
                                               new PushNotificationConsts.LiveMapPushData(),
                                               student.ParentID,
                                               null);
                }
                else
                {
                    result = Error.Conflict("UpdateStudentStatusOffBus", "The bus should be in service and have a valid destination point to update the current student place.");
                }
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost("get-student-bus-location")]
        public async Task<IActionResult> getStudentBusLocation(GetStudentBusLocationDto getStudentBusLocationDto)
        {
            ErrorOr<string> result = "";
            Parent parent = await parentRepository.GetParentById(getStudentBusLocationDto.ParentID);

            if (parent == null)
            {
                result = Error.NotFound("GetStudentBusLocation", string.Format("No parent is found with the given ID({0}).", getStudentBusLocationDto.ParentID));
            }
            else
            {
                List<Student> parentChildren = (List<Student>)parent.Students;
                if (parentChildren == null || parentChildren.Count == 0)
                {
                    result = Error.NotFound("GetStudentBusLocation", "The given parent ID does not have any linked childrens.");
                }
                else
                {
                    foreach (Student child in parentChildren)
                    {
                        if (child.IsOnBus)
                        {
                            Bus bus = await busRepository.GetBusById((int)child.BusID);
                            if (bus == null)
                            {
                                result = Error.NotFound("GetStudentBusLocation", string.Format("No bus is found with the given ID({0}).", (int)child.BusID));
                            }
                            else
                            {
                                result = bus.CurrentLocation;
                                break;
                            }
                        }
                        else
                        {
                            result = Error.NotFound("GetStudentBusLocation", "No children were found on the bus.");
                        }
                    }
                }
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}
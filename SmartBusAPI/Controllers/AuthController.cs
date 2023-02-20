namespace SmartBusAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IJwtAuthService jwtAuthService;
        private readonly IParentRepository parentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IBusRepository busRepository;
        private readonly IBusDriverRepository busDriverRepository;
        private readonly IHashProviderService hashProviderService;
        
        public AuthController(IMapper mapper,
                              IJwtAuthService jwtAuthService,
                              IParentRepository parentRepository, 
                              IStudentRepository studentRepository, 
                              IBusRepository busRepository,
                              IBusDriverRepository busDriverRepository,
                              IHashProviderService hashProviderService)
        {
            this.mapper = mapper;
            this.jwtAuthService = jwtAuthService;
            this.parentRepository = parentRepository;
            this.studentRepository = studentRepository;
            this.busRepository = busRepository;
            this.busDriverRepository = busDriverRepository;
            this.hashProviderService = hashProviderService;
        }

        [HttpPost("login/parent")]
        public async Task<IActionResult> Login(LoginParentRequestDto loginParentDto)
        {
            ErrorOr<LoginParentResponseDto> result;
            Parent parent = await parentRepository.GetParentByEmail(loginParentDto.Email);

            if (parent == null)
            {
                result = Error.NotFound(code: "ParentEmailNotFound", description: "The given email does not exist.");
            }
            else if (!hashProviderService.VerifyHash(loginParentDto.Password, parent.Password))
            {
                result = Error.NotFound(code: "InvalidParentPassword", description: "Password is incorrect");
            }
            else
            {
                string authToken = jwtAuthService.GenerateAuthToken(parent.FirstName, parent.LastName, nameof(Parent));

                LoginParentResponseDto loginParentResponseDto = new()
                {
                    ParentDto = mapper.Map<ParentDto>(parent),
                    AuthToken = authToken
                };

                result = loginParentResponseDto;
            }
            
            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost("login/bus-driver")]
        public async Task<IActionResult> Login(LoginDriverRequestDto loginDriverDto)
        {
            ErrorOr<LoginDriverResponseDto> result;
            BusDriver busDriver = await busDriverRepository.GetBusDriverByDriverID(loginDriverDto.DriverID);
            Bus bus = await busRepository.GetBusById((int)busDriver.BusID);

            if (busDriver == null)
            {
                result = Error.NotFound(code: "DriverIDNotFound", description: "The given Driver ID does not exist.");
            }
            else if (!hashProviderService.VerifyHash(loginDriverDto.Password, busDriver.Password))
            {
                result = Error.NotFound(code: "InvalidDriverPassword", description: "Password is incorrect");
            }
            else
            {
                string authToken = jwtAuthService.GenerateAuthToken(busDriver.FirstName, busDriver.LastName, nameof(BusDriver));
                LoginDriverResponseDto loginDriverResponseDto = new()
                {
                    BusDriverDto = mapper.Map<BusDriverDto>(busDriver),
                    BusDto = mapper.Map<BusDto>(bus),
                    AuthToken = authToken
                };
                result = loginDriverResponseDto;
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }
 
        [HttpPost("register/parent")]
        public async Task<IActionResult> Register(ParentRegisterDto parentRegisterDto)
        {
            ErrorOr<string> result;
            if (await parentRepository.GetParentByEmail(parentRegisterDto.ParentEmail) is not null)
            {
                result = Error.Conflict(code: "DuplicateEmailParent", description: "The given email already exists");
            }
            else
            {
                Parent parent = new()
                {
                    FirstName = parentRegisterDto.ParentFirstName,
                    LastName = parentRegisterDto.ParentLastName,
                    Email = parentRegisterDto.ParentEmail,
                    PhoneNumber = parentRegisterDto.ParentPhoneNumber,
                    Address = parentRegisterDto.ParentAddress,
                    Password = hashProviderService.ComputeHash(parentRegisterDto.Password)
                };

                await parentRepository.AddParent(parent);
                result = "Parent is registered successfully";
            }

            return result.Match
            (
                Ok,
                Problem
            );
        }

        [HttpPost("register/student")]
        public async Task<IActionResult> Register(StudentRegisterDto studentRegisterDto)
        {
            Parent parent = await parentRepository.GetParentByEmail(studentRegisterDto.ParentEmail);

            if (parent == null)
            {
                parent = new Parent
                {
                    FirstName = studentRegisterDto.ParentFirstName,
                    LastName = studentRegisterDto.ParentLastName,
                    Email = studentRegisterDto.ParentEmail,
                    PhoneNumber = studentRegisterDto.ParentPhoneNumber,
                    Address = studentRegisterDto.ParentAddress,
                    Password = studentRegisterDto.Password
                };
                await parentRepository.AddParent(parent);
            }

            Student student = new()
            {
                FirstName = studentRegisterDto.FirstName,
                LastName = studentRegisterDto.LastName,
                Gender = studentRegisterDto.Gender,
                GradeLevel = studentRegisterDto.GradeLevel,
                Address = studentRegisterDto.ParentAddress,
                BelongsToBusID = studentRegisterDto.BelongsToBusID,
                IsAtSchool = studentRegisterDto.IsAtSchool,
                IsAtHome = studentRegisterDto.IsAtHome,
                IsOnBus = studentRegisterDto.IsOnBus,
                ParentID = parent.ID
            };

            await studentRepository.AddStudent(student);

            return Ok("Student is registered successfully");
        }

        [HttpPost("register/bus-driver")]
        public async Task<IActionResult> Register(DriverRegisterDto driverRegisterDto)
        {
            ErrorOr<string> result;
            if (await busDriverRepository.GetBusDriverByDriverID(driverRegisterDto.DriverID) is not null)
            {
                result = Error.Conflict(code: "DuplicateDriverID", description: "The given Driver ID already exists");
            }
            else
            {
                BusDriver busDriver = new()
                {
                    FirstName = driverRegisterDto.FirstName,
                    LastName = driverRegisterDto.LastName,
                    Email = driverRegisterDto.Email,
                    DriverID = driverRegisterDto.DriverID,
                    PhoneNumber = driverRegisterDto.PhoneNumber,
                    Country = driverRegisterDto.Country,
                    Password = hashProviderService.ComputeHash(driverRegisterDto.Password)
                };

                await busDriverRepository.AddBusDriver(busDriver);

                result = "Bus Driver is registered successfully";
            }
    
            return result.Match
            (
                Ok,
                Problem
            );
        }
    }
}
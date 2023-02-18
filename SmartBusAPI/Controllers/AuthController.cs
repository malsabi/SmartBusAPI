using ErrorOr;
using SmartBusAPI.Entities;
using SmartBusAPI.DTOs.Bus;
using Microsoft.AspNetCore.Mvc;
using SmartBusAPI.DTOs.Auth.Login;
using SmartBusAPI.DTOs.Auth.Registration;
using SmartBusAPI.Common.Interfaces.Services;
using SmartBusAPI.Common.Interfaces.Repositories;

namespace SmartBusAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IJwtAuthService jwtAuthService;
        private readonly IParentRepository parentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IBusDriverRepository busDriverRepository;
        private readonly IHashProviderService hashProviderService;
        
        public AuthController(IJwtAuthService jwtAuthService,
                              IParentRepository parentRepository, 
                              IStudentRepository studentRepository, 
                              IBusDriverRepository busDriverRepository,
                              IHashProviderService hashProviderService)
        {
            this.jwtAuthService = jwtAuthService;
            this.parentRepository = parentRepository;
            this.studentRepository = studentRepository;
            this.busDriverRepository = busDriverRepository;
            this.hashProviderService = hashProviderService;
        }

        [HttpPost("login/parent")]
        public async Task<IActionResult> Login(LoginParentDto loginParentDto)
        {
            ErrorOr<string> result;
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
                string authToken = jwtAuthService.GenerateAuthToken(parent.ParentFirstName, parent.ParentLastName, nameof(Parent));
                result = authToken;
            }
            
            return result.Match
            (
                success => Ok(success),
                errors => Problem(errors)
            );
        }

        [HttpPost("login/bus-driver")]
        public async Task<IActionResult> Login(LoginDriverDto loginDriverDto)
        {
            ErrorOr<LoginDriverResponseDto> result;
            BusDriver busDriver = await busDriverRepository.GetBusDriverByDriverID(loginDriverDto.DriverID);

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
                    ID = busDriver.ID,
                    FirstName = busDriver.FirstName,
                    LastName = busDriver.LastName,
                    Email = busDriver.Email,
                    DriverID = busDriver.DriverID,
                    PhoneNumber = busDriver.PhoneNumber,
                    Country = busDriver.Country,
                    BusDto = new BusDto()
                    {
                        ID = (int)busDriver.BusID,
                        BusNumber = busDriver.Bus.BusNumber,
                        Capacity = busDriver.Bus.Capacity,
                        CurrentLocation = busDriver.Bus.CurrentLocation,
                    },
                    Token = authToken
                };

                result = loginDriverResponseDto;
            }

            return result.Match
            (
                success => Ok(success),
                errors => Problem(errors)
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
                    ParentFirstName = parentRegisterDto.ParentFirstName,
                    ParentLastName = parentRegisterDto.ParentLastName,
                    ParentEmail = parentRegisterDto.ParentEmail,
                    ParentPhoneNumber = parentRegisterDto.ParentPhoneNumber,
                    ParentAddress = parentRegisterDto.ParentAddress,
                    Password = hashProviderService.ComputeHash(parentRegisterDto.Password)
                };

                await parentRepository.AddParent(parent);
                result = "Parent is registered successfully";
            }

            return result.Match
            (
                success => Ok(success),
                errors => Problem(errors)
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
                    ParentFirstName = studentRegisterDto.ParentFirstName,
                    ParentLastName = studentRegisterDto.ParentLastName,
                    ParentEmail = studentRegisterDto.ParentEmail,
                    ParentPhoneNumber = studentRegisterDto.ParentPhoneNumber,
                    ParentAddress = studentRegisterDto.ParentAddress,
                    Password = studentRegisterDto.Password
                };
                await parentRepository.AddParent(parent);
            }

            Student student = new()
            {
                FaceRecognitionID = studentRegisterDto.FaceRecognitionID,
                FirstName = studentRegisterDto.FirstName,
                LastName = studentRegisterDto.LastName,
                Gender = studentRegisterDto.Gender,
                SchoolName = studentRegisterDto.SchoolName,
                GradeLevel = studentRegisterDto.GradeLevel,
                Parent = parent
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
                success => Ok(success),
                errors => Problem(errors)
            );
        }
    }
}
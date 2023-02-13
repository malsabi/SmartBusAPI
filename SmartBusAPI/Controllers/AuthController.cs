using SmartBusAPI.Entities;
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
            Parent parent = await parentRepository.GetParentByEmail(loginParentDto.Email);

            if (parent == null)
            {
                return BadRequest("Email does not exists");
            }

            if (!hashProviderService.VerifyHash(loginParentDto.Password, parent.Password))
            {
                return BadRequest("Password is incorrect");
            }

            string authToken = jwtAuthService.GenerateAuthToken(parent.ParentFirstName, parent.ParentLastName, nameof(parent));

            return Ok(authToken);
        }

        [HttpPost("login/bus-driver")]
        public async Task<IActionResult> Login(LoginDriverDto loginDriverDto)
        {
            BusDriver busDriver = await busDriverRepository.GetBusDriverByDriverID(loginDriverDto.DriverID);

            if (busDriver == null)
            {
                return BadRequest("Driver ID does not exists");
            }

            if (!hashProviderService.VerifyHash(loginDriverDto.Password, busDriver.Password))
            {
                return BadRequest("Password is incorrect");
            }

            string authToken = jwtAuthService.GenerateAuthToken(busDriver.FirstName, busDriver.LastName, nameof(BusDriver));

            return Ok(authToken);
        }
 
        [HttpPost("register/parent")]
        public async Task<IActionResult> Register(ParentRegisterDto parentRegisterDto)
        {
            if (await parentRepository.GetParentByEmail(parentRegisterDto.ParentEmail) is not null)
            {
                return BadRequest("Email already exists");
            }

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

            return Ok();
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

            Student student = new Student
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

            return Ok();
        }

        [HttpPost("register/bus-driver")]
        public async Task<IActionResult> Register(DriverRegisterDto driverRegisterDto)
        {
            if (await busDriverRepository.GetBusDriverByDriverID(driverRegisterDto.DriverID) is not null)
            {
                return BadRequest("Driver ID already exists");
            }

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

            return Ok();
        }
    }
}
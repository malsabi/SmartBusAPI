using SmartBusAPI.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace SmartBusAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        public AuthController()
        {
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            if (loginDto.Role == "Parent")
            {
                return Ok(loginDto);
            }
            else if (loginDto.Role == "Student")
            {
                return Ok(loginDto);
            }
            else if (loginDto.Role == "Administrator")
            {
                return Ok(loginDto);
            }
            return BadRequest();
        }

        [HttpPost("register/parent")]
        public IActionResult Register(ParentRegisterDto parentRegisterDto)
        {
            return Ok(parentRegisterDto);
        }

        [HttpPost("register/student")]
        public IActionResult Register(StudentRegisterDto studentRegisterDto)
        {
            return Ok(studentRegisterDto);
        }
    }
}
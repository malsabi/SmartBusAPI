using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace SmartBusAPI.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
            {
                return Problem();
            }
            return Problem(errors[0]);
        }

        private IActionResult Problem(Error error)
        {
            int statusCode;
            switch (error.Type)
            {
                case ErrorType.Conflict:
                    statusCode = StatusCodes.Status409Conflict;
                    break;
                case ErrorType.Validation:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case ErrorType.NotFound:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            return Problem(statusCode: statusCode, title: error.Code, detail: error.Description, type: error.Type.ToString());
        }
    }
}
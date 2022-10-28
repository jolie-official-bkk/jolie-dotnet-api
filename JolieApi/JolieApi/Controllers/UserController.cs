using System.Net;
using JolieApi.DataContext;
using JolieApi.Models;
using JolieApi.Repository;
using JolieApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEENApiV2_Admin.Repository;

namespace JolieApi.Controllers
{
    [ApiController]
    [Route("/api/user/[action]")]
    public class UserController : ControllerBase
    {
        //private readonly IJWTManagerRepository _jWTManager;
        private readonly IUserManagerRepository _userManager;

        public UserController(IUserManagerRepository userManager, IJWTManagerRepository jWTManagerRepository)
        {
            this._userManager = userManager;
            //this._jWTManager = jWTManagerRepository;
        }

        //[HttpGet]
        //public IActionResult getToken([FromQuery] string email)
        //{
        //    return Ok(_jWTManager.GenerateJwtToken(email));
        //}

        //[HttpGet]
        //public IActionResult validateToken([FromQuery] string token)
        //{
        //    return Ok(_jWTManager.ValidateToken(token));
        //}

        [Authorize]
        [HttpGet]
        public IActionResult getUsers()
        {
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "success",
                data = _userManager.GetUsers(),
            });
        }

        [HttpGet]
        public IActionResult ping()
        {
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "success"
            });
        }

        [HttpPost]
        public IActionResult login([FromBody] LoginRequest requestBody)
        {
            string response = _userManager.Login(requestBody);

            if (response == "")
            {
                return Unauthorized();
            }
            else
            {
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    message = "success",
                    data = new
                    {
                        token = response
                    },
                });
            }
        }

        [HttpPost]
        public IActionResult register([FromBody] RegisterRequest requestBody)
        {
            string token = _userManager.Register(requestBody);

            if (token == "existed")
            {
                return Conflict(new
                {
                    status = HttpStatusCode.Conflict,
                    message = "email '" + requestBody.email + "' is already existed",
                });
            }

            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "success",
                data = new
                {
                    token = token
                }
            });
        }
    }
}



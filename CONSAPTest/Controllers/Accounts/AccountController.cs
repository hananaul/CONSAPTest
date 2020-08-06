using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CONSAPTest.Dto;
using CONSAPTest.Models;
using CONSAPTest.Services.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CONSAPTest.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountService _userService;

        public AccountController(AccountService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public ActionResult<IEnumerable<LoginResultDto>> Login([FromBody]LoginInputDto input)
        {
            var result = _userService.Login(input);

            return Ok(result);
        }

        [HttpPost("RegisterUser")]
        public ActionResult<IEnumerable<User>> RegisterUser([FromBody]RegisterUserDto input)
        {
            var result = _userService.RegisterUser(input);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetListUser")]
        public ActionResult<IEnumerable<List<GetListUserResultDto>>> GetListUser()
        {
            var result = _userService.GetListUser();

            return Ok(result);
        }
    }
}
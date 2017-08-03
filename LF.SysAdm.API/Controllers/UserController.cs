using LF.SysAdm.API.Controllers.Base;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Command.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LF.SysAdm.API.Controllers
{
    // api/controller/parametros
    [Route("api")]
    public class UserController :BaseController
    {
        private readonly IBusinessUser _service;
        public UserController(IBusinessUser service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("v1/users")]
        [AllowAnonymous]
        //[Authorize(Policy ="Users")]
        //[Authorize(Policy ="Users")]

        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var user = User.Identity.Name;
                var result = _service.GetUsers();
                return await CreateResponse(result);
            }
            catch (System.Exception ex)
            {
                return  Ok( ex.Message.ToString());
            }         
        }

        [HttpPost]
        [Route("v1/users")]
        [AllowAnonymous]
        public async Task<IActionResult> PostUser([FromBody]dynamic body)
        {
            try
            {
                var Cmd = new RegisterUsersCommand
                {
                    Name = (string)body.name,
                    Email = (string)body.email,
                    Password = (string)body.password
                };

              var result =   _service.NewUser(Cmd);

                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpPut]
        [Route("v1/users")]
        [AllowAnonymous]
        public async Task<IActionResult> PutUser([FromBody]dynamic body)
        {
            try
            {
                var cmd = new EditUserCommand
                {
                    ID = (Guid)body.id,
                    Name = (string)body.name,
                    Email = (string)body.email,
                    Password = (string)body.password
                };

                var result = _service.UpdateUser(cmd);

                return await CreateResponse(result);
            }
            catch (Exception ex)
            {

                return await ServerErroApp(ex);
            }
        }

        [HttpGet]
        [Route("v1/users/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var result = _service.GetUser(id);

                return await CreateResponse(result);
            }
            catch (Exception ex)
            {

                return await ServerErroApp(ex);
            }
        }

        [HttpDelete]
        [Route("v1/users/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = _service.Remove(id);

                return await CreateResponse(result);
            }
            catch (Exception ex)
            {

                return await ServerErroApp(ex);
            }
        }


    }
}

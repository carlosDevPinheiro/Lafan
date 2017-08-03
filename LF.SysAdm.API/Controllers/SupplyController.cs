using LF.SysAdm.API.Controllers.Base;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Command.Supply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LF.SysAdm.API.Controllers
{
    [Route("api")]
    public class SupplyController: BaseController
    {
        private readonly IBusinessSupply _service;
        public SupplyController(IBusinessSupply service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("v1/supply")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSupplyes()
        {
            try
            {
                var result = _service.GetAllSupplys();
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpGet]
        [Route("v1/supply/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSupplye(Guid Id)
        {
            try
            {
                var result = _service.GetSupply(Id);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpGet]
        [Route("v1/supply/{cnpj}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSupplye(string cnpj)
        {
            try
            {
                var result = _service.GetSupply(cnpj);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpPost]
        [Route("v1/supply")]
        [AllowAnonymous]
        public async Task<IActionResult> PostSupply([FromBody] dynamic body)
        {
            try
            {
                var command = new RegisterSupplyCommand
                {
                    Agent = (string)body.agent,
                    CEP = (string)body.cep,
                    City = (string)body.city,
                    CNPJ = (string)body.cnpj,
                    CompanyName = (string)body.conpanyName,
                    Complement = (string)body.complement,
                    District = (string)body.district,
                    Email = (string)body.email,
                    Number = (int)body.number,
                    Phone = (string)body.phone,
                    State = (string)body.state,
                    Street = (string)body.street
                };

                var result = _service.NewSupply(command);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

    }
}

using LF.SysAdm.API.Controllers.Base;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Command.Address;
using LF.SysAdm.Domain.Command.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LF.SysAdm.API.Controllers
{
    [Route("api")]
    public class CustomerController : BaseController
    {
        private readonly IBusinessCustomer _service;
        public CustomerController(IBusinessCustomer service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("v1/customer")]
        [AllowAnonymous]
        public async Task<IActionResult> PostCustomer([FromBody] dynamic body)
        {
            try
            {
                var cmd = new RegisterCustomerCommand
                {
                    CEP = (string)body.cep,
                    City = (string)body.city,
                    Complement = (string)body.complement,
                    District = (string)body.district,
                    Number = (int)body.number,
                    Street = (string)body.street,
                    State = (string)body.state,
                    DateBirthday = (DateTime)body.dateBirthday,
                    Document = (string)body.document,
                    // Email = User.Identity.Name,
                    Email = (string)body.email,
                    Gender = (bool)body.gender,
                    Phone = (string)body.phone,
                    UserId = (Guid)body.userId
                };

                var result = _service.NewCustomer(cmd);

                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpGet]
        [Route("v1/customer/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomer(Guid Id)
        {
            try
            {
                var result = _service.GetCustomer(Id);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpGet]
        [Route("v1/customer/{cpf}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomer(string cpf)
        {
            try
            {
                var result = _service.GetCustomer(cpf);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpPut]
        [Route("v1/customer")]
        [AllowAnonymous]
        public async Task<IActionResult> PutCustomer([FromBody] dynamic body)
        {
            try
            {
                var cmd = new EditCustomerCommand
                {
                    DateBirthday = (DateTime)body.dateBirthday,
                    Document = (string)body.document,
                    CustomerId = (Guid)body.customerId,
                    Gender = (bool)body.gender,
                    Phone = (string)body.phone,
                };

                var result = _service.EditeCustomer(cmd);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpGet]
        [Route("v1/customer/{id}/user")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomersWithUser(Guid Id)
        {
            try
            {
                var result = _service.GetCustomerWithUser(Id);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpGet]
        [Route("v1/customer/{id}/address")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomerWithAddress(Guid Id)
        {
            try
            {
                var result = _service.GetCustomerWithAddress(Id);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpPost]
        [Route("v1/customer/new/address")]
        [AllowAnonymous]
        public async Task<IActionResult> AddAddressCustomer([FromBody] dynamic body)
        {
            try
            {
                var cmd = new RegisterAddressCommand
                {
                    CEP = (string)body.cep,
                    City = (string)body.city,
                    Complement = (string)body.complement,
                    CustomerId = (Guid)body.customerId,
                    District = (string)body.district,
                    Number = (int)body.number,
                    State = (string)body.state,
                    Street = (string)body.street
                };
                var result = _service.NewAddressCustomer(cmd);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpPut]
        [Route("v1/customer/edit/address")]
        [AllowAnonymous]
        public async Task<IActionResult> EditAddressCustomer([FromBody] dynamic body)
        {
            try
            {
                var cmd = new EditeAddressCommand
                {
                    CEP = (string)body.cep,
                    City = (string)body.city,
                    Complement = (string)body.complement,
                    AddressId = (Guid)body.addressId,
                    District = (string)body.district,
                    Number = (int)body.number,
                    State = (string)body.state,
                    Street = (string)body.street
                };
                var result = _service.EditeAddressCustomer(cmd);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }

        [HttpDelete]
        [Route("v1/customer/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCustomer(Guid Id)
        {
            try
            {
                var result = _service.GetCustomerWithAddress(Id);
                return await CreateResponse(result);
            }
            catch (Exception ex)
            {
                return await ServerErroApp(ex);
            }
        }
    }
}

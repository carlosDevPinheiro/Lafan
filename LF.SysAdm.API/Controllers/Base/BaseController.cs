using LF.SysAdm.Domain.Querys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LF.SysAdm.API.Controllers.Base
{
    public class BaseController : Controller
    {

        public async Task<IActionResult> CreateResponse(ObjectRequest obj)
        {           

            try
            {
                if (obj.Data != null)
                {
                   return await Task.FromResult(Ok(new
                    {
                        data = obj.Data,
                        succes = obj.Success
                    }));
                }

                return await Task.FromResult(Ok(new
                {
                    success = obj.Success,
                    error = obj.Errors
                }));
               
            }
            catch
            {
                throw;
            }
        }

        public async Task<IActionResult> ServerErroApp(Exception ex)
        {
            // logar erro com elma

           return await Task.FromResult( BadRequest(  "Ocorreu um erro interno em Lafan.com.br" ));
        }
    }
}

 
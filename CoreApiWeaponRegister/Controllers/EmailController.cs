using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
using CoreApiWeaponRegister.Entities.Models.Correo;
using CoreApiWeaponRegister.Repository;
using CoreApiWeaponRegister.Repository.Interfaces;

namespace CoreApiWeaponRegister.Controllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class EmailController : Controller
    {
        private readonly IMailService mailService;

        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }


        [HttpPost("Send")]
        //public async Task<IActionResult> Send([FromForm] MailRequest request)
        //public async Task<IActionResult> Send([FromBody] MailRequest request) 
        public bool Send([FromBody] EmailData request)
        {
            try
            {
                //mailService.SendEmailAsyncV2(request);
                //return Ok();
                return mailService.SendEmail(request);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}

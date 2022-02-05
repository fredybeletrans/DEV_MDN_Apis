using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWeaponRegister.Entities.Models.Correo;

namespace CoreApiWeaponRegister.Repository.Interfaces
{
    public interface IMailService
    {
        //Task SendEmailAsync(MailRequest mailRequest);
        //MailRequest SendEmailAsyncV2(MailRequest mailRequest);

        bool SendEmail(EmailData emailData);
    }
}

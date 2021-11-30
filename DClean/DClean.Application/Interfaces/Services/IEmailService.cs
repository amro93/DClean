using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.Email;

namespace DClean.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}

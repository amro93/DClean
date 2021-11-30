using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.Countries;
using DClean.Application.DTOs.StaticFiles;

namespace DClean.Application.DTOs.DemoRequests
{
    public class DemoRequestCreateDto
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string SaaClientDefault { get; set; }
        public Guid? CompanyCountryId { get; set; }
        public Base64FileVM CompanyLogo { get; set; }
    }

    public class DemoRequestUpdateDto : DemoRequestCreateDto
    {
        public Guid Id { get; set; }
    }
}

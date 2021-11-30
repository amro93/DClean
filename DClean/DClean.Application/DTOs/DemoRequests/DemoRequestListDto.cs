using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.StaticFiles;

namespace DClean.Application.DTOs.DemoRequests
{
    public class DemoRequestListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string SaaClientDefault { get; set; }
        public Guid? CompanyCountryId { get; set; }
        public string CompanyCountryName { get; set; }
        public string CompanyLogo { get; set; }
    }

    public class DemoRequestDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string SaaClientDefault { get; set; }
        public Guid? CompanyCountryId { get; set; }
        public string CompanyCountryName { get; set; }
        public string CompanyLogo { get; set; }
    }
}

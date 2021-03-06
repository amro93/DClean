using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Common.BaseEntities;
using DClean.Infrastructure.Common.SharedEntities;
using DClean.Infrastructure.Persistence.Models.Location;

namespace DClean.Infrastructure.Persistence.Onboarding.Models
{
    public class DemoRequest : SoftDeleteAuditedEntity
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string SaaClientDefault { get; set; }
        public EDemoRequestState State { get; set; }
        public Guid? CompanyCountryId { get; set; }
        public virtual Country CompanyCountry { get; set; }
        public Guid? CompanyLogoId { get; set; }
        public virtual StaticFileInfo CompanyLogo { get; set; }

    }

    public enum EDemoRequestState
    {
        New,
        Approved,
        OnHold,
        Rejected
    }
}

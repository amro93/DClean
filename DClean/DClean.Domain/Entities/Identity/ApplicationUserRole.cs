using System;
using DClean.Domain.Interfaces;
using DClean.Infrastructure.Common.BaseEntities.Identity;

namespace DClean.Domain.Entities.Presistence.Identity
{
    public class ApplicationUserRole : BaseUserRole<ApplicationUser, ApplicationRole>, IEntity, IMayHaveTenant
    {
        public Guid? TenantId { get; set; }

    }
}

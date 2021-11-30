using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTower.Application.Interfaces;
using VTower.Application.Interfaces.Identity;
using VTower.Infrastructure.Common.DbTriggers;
using VTower.Infrastructure.Persistence.Contexts;

namespace VTower.Infrastructure.MultiTenancy.Contexts.Triggers
{
    public class UpdateAuditedEntityTrigger : BaseUpdateAuditedEntityTrigger
    {
        public UpdateAuditedEntityTrigger(ICurrentUser userService, IDateTimeService dateTimeService, TenantDbContext dbContext) : base(userService, dateTimeService, dbContext)
        {
        }
    }
}

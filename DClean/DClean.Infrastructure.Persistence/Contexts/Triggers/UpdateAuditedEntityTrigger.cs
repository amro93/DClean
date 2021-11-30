using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Interfaces;
using DClean.Application.Interfaces.Identity;
using DClean.Domain.Interfaces;
using DClean.Infrastructure.Common.DbTriggers;

namespace DClean.Infrastructure.Persistence.Contexts.Triggers
{
    public class UpdateAuditedEntityTrigger : BaseUpdateAuditedEntityTrigger, IBeforeSaveTrigger<IUpdateAuditedEntity<Guid>>
    {
        public UpdateAuditedEntityTrigger(ICurrentUser userService, ICurrentTenant currentTenant, IDateTimeService dateTimeService, DCleanDbContext dbContext) : base(userService, currentTenant, dateTimeService, dbContext)
        {
        }
    }

    public class CreationAuditedEntityTrigger : BaseCreationAuditedEntityTrigger, IBeforeSaveTrigger<ICreateAuditedEntity<Guid>>, IBeforeSaveTrigger<IMayHaveTenant>, IBeforeSaveTrigger<IHaveTenant>
    {
        public CreationAuditedEntityTrigger(ICurrentUser userService, ICurrentTenant currentTenant, IDateTimeService dateTimeService) : base(userService, currentTenant, dateTimeService)
        {
        }
    }

    public class SoftDeletableAuditedTrigger : BaseSoftDeletableAuditedTrigger, IBeforeSaveTrigger<ISoftDeleteAuditedEntity<Guid>>
    {
        public SoftDeletableAuditedTrigger(ICurrentUser authenticatedUser) : base(authenticatedUser)
        {
        }
    }

    public class SoftDeletableTrigger : BaseSoftDeletableTrigger, IBeforeSaveTrigger<ISoftDeleteEntity>
    {
        public SoftDeletableTrigger(IDateTimeService dateTimeService) : base(dateTimeService)
        {
        }
    }

    public class TrackedAuditedEntityTrigger : BaseTrackedAuditedEntityTrigger, IBeforeSaveTrigger<ITrackedEntity>
    {
        public TrackedAuditedEntityTrigger(ICurrentUser currentUser, IDateTimeService dateTimeService, DCleanDbContext dbContext, DCleanDbContext trackingContext) : base(currentUser, dateTimeService, dbContext, trackingContext)
        {
        }
    }
}

using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DClean.Application.Interfaces;
using DClean.Application.Interfaces.Identity;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.DbTriggers
{
    public abstract class BaseUpdateAuditedEntityTrigger : IBeforeSaveTrigger<IUpdateAuditedEntity<Guid?>>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTimeService _dateTimeService;

        private readonly DbContext _dbContext;
        public BaseUpdateAuditedEntityTrigger(ICurrentUser currentUser,
            ICurrentTenant currentTenant,
            IDateTimeService dateTimeService, DbContext dbContext)
        {
            _currentUser = currentUser;
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }
        public virtual async Task BeforeSave(ITriggerContext<IUpdateAuditedEntity<Guid?>> context, CancellationToken cancellationToken)
        {

            if (context.ChangeType != ChangeType.Modified) return;
            var hasUpdatedProperties = true; // _dbContext.Entry(entryType).CurrentValues.Properties.Where(t => t.Name != nameof(ISoftDeleteEntity.IsDeleted)).Any();
            if (!hasUpdatedProperties) return;
            context.Entity.UpdatedAt = _dateTimeService.NowUtc;
            context.Entity.UpdatedById = _currentUser.UserId;
        }
    }

}

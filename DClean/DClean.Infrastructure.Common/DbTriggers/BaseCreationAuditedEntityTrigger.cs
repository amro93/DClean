using EntityFrameworkCore.Triggered;
using System;
using System.Threading;
using System.Threading.Tasks;
using DClean.Application.Interfaces;
using DClean.Application.Interfaces.Identity;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.DbTriggers
{
    public abstract class BaseCreationAuditedEntityTrigger : IBeforeSaveTrigger<ICreateAuditedEntity<Guid>>, IBeforeSaveTrigger<IMayHaveTenant>, IBeforeSaveTrigger<IHaveTenant>
    {
        private readonly ICurrentUser _userService;
        private readonly ICurrentTenant _currentTenant;
        private readonly IDateTimeService _dateTimeService;

        public BaseCreationAuditedEntityTrigger(ICurrentUser userService,
            ICurrentTenant currentTenant,
            IDateTimeService dateTimeService)
        {
            _userService = userService;
            _currentTenant = currentTenant;
            _dateTimeService = dateTimeService;
        }


        public async Task BeforeSave(ITriggerContext<ICreateAuditedEntity<Guid>> context,
            CancellationToken cancellationToken)
        {
            if (context.ChangeType != ChangeType.Added) return;
            context.Entity.CreatedAt = _dateTimeService.NowUtc;
            context.Entity.CreatedById = _userService.UserId;
        }

        public async Task BeforeSave(ITriggerContext<IMayHaveTenant> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType != ChangeType.Added) return;
            context.Entity.TenantId = _currentTenant.TenantId;
        }

        public async Task BeforeSave(ITriggerContext<IHaveTenant> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType != ChangeType.Added) return;
            context.Entity.TenantId = _currentTenant.GetTenantId();
        }
    }

    //public class TrackedAuditedEntityTrigger : IBeforeSaveTrigger<ITrackedEntity>
    //{
    //    private readonly IAuthenticatedUserService _userService;
    //    private readonly IDateTimeService _dateTimeService;

    //    public TrackedAuditedEntityTrigger(IAuthenticatedUserService userService, IDateTimeService dateTimeService)
    //    {
    //        _userService = userService;
    //        _dateTimeService = dateTimeService;
    //    }

    //    public Task BeforeSave(ITriggerContext<ITrackedEntity> context, CancellationToken cancellationToken)
    //    {
    //        return Task.CompletedTask;
    //    }
    //}

}

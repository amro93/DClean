using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DClean.Application.Interfaces;
using DClean.Application.Interfaces.Identity;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.DbTriggers
{
    public abstract class BaseTrackedAuditedEntityTrigger : IBeforeSaveTrigger<ITrackedEntity>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTimeService _dateTimeService;
        private readonly DbContext _dbContext;
        private readonly DbContext _trackingContext;

        public BaseTrackedAuditedEntityTrigger(ICurrentUser currentUser,
            IDateTimeService dateTimeService,
            DbContext dbContext, DbContext trackingContext) // Add request context
        {
            _currentUser = currentUser;
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
            _trackingContext = trackingContext;
        }

        public async Task BeforeSave(ITriggerContext<ITrackedEntity> context, CancellationToken cancellationToken)
        {
            //_dbContext.Entry(context.GetType()).
            // TODO: track changes and put into database audit logs
        }
    }

}

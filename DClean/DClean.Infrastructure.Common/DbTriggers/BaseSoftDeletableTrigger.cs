using EntityFrameworkCore.Triggered;
using EntityFrameworkCore.Triggered.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DClean.Application.Interfaces;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.DbTriggers
{
    public abstract class BaseSoftDeletableTrigger : IBeforeSaveTrigger<ISoftDeleteEntity>
    {
        private readonly IDateTimeService _dateTimeService;

        public BaseSoftDeletableTrigger(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }
        public async Task BeforeSave(ITriggerContext<ISoftDeleteEntity> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType != ChangeType.Modified ||
                !context.Entity.IsDeleted || context.UnmodifiedEntity.IsDeleted) return;
            context.Entity.DeletedAt = _dateTimeService.NowUtc;
        }
    }

}

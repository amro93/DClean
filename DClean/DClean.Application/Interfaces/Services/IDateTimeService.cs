using System;
using System.Collections.Generic;
using System.Text;

namespace DClean.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}

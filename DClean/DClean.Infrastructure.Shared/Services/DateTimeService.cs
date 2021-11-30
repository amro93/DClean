using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using DClean.Application.Interfaces;

namespace DClean.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        private DateTime? _nowUtc = null;
        public DateTime NowUtc
        {
            get
            {
                if (!_nowUtc.HasValue) _nowUtc = DateTime.UtcNow;
                return _nowUtc.Value;
            }
        }

        private DateTimeOffset? _nowOffsetUtc = null;
        public DateTimeOffset NowOffsetUtc
        {
            get
            {
                if (!_nowOffsetUtc.HasValue) _nowOffsetUtc = DateTimeOffset.UtcNow;
                return _nowOffsetUtc.Value;
            }
        }
    }
}

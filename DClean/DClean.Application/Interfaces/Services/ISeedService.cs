using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Application.Interfaces
{
    public interface ISeedService
    {
        public int Order { get; }
        Task SeedAsync();
    }
}

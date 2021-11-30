using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using DClean.Domain.Interfaces;
using DClean.Infrastructure.Persistence.Onboarding.Models;

namespace InfraStructure.Presistance.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var x = typeof(IEntity<Guid>).IsAssignableFrom(typeof(DemoRequest));
            var tType = typeof(IEntity<Guid>);
            var eqq = tType.IsAssignableFrom(typeof(DemoRequest));
            var interfaces = typeof(DemoRequest).GetInterfaces().ToList();
            foreach(var inter in interfaces.Where(i => i.Name.StartsWith("IEntity")))
            {
                var x = inter.FullName;
                var x1 = tType.FullName;

                var eq = inter == tType;
            }
        }
    }
}

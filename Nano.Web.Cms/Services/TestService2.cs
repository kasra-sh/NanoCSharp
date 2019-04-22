using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nano.Web.Cms.Services
{
    public abstract class ATestService2
    {
        public abstract string GetId();
    }
    public class TestService2: ATestService2
    {
        private readonly ATestService _testService;

        public TestService2(ATestService testService)
        {
            _testService = testService;
        }
        public override string GetId()
        {
            return _testService.GetId();
        }
    }
}

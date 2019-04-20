using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nano.Web.Cms.Services
{
    public abstract class ATestService
    {
        protected ATestService()
        {
            
        }

        public string GetName()
        {
            return this.GetType().Name;
        }
    }

    public class TestService : ATestService
    {
        public TestService(): base()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nano.Web.Cms.Services
{
    public abstract class ATestService
    {
        private string _id;
        protected ATestService()
        {
            
        }

        public string GetName()
        {
            return this.GetType().Name;
        }

        public void SetId(string id)
        {
            _id = id;
        }

        public string GetId()
        {
            return _id;
        }
    }

    public class TestService : ATestService
    {
        public TestService(): base()
        {
            
        }
    }
}

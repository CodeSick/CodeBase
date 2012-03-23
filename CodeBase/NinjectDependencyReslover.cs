using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;

namespace CodeBase
{
    public class NinjectDependencyReslover  : IDependencyResolver
    {
        private readonly IResolutionRoot _root;

        public NinjectDependencyReslover(IResolutionRoot kernel)
        {
            _root = kernel;
        }

        public Object GetService(Type serviceType)
        {
            return _root.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _root.GetAll(serviceType);
        }
    }
}
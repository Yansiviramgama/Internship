using Login.Repository.Repository;
using Login.Repository.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Login
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ILoginInterface, LoginServices>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
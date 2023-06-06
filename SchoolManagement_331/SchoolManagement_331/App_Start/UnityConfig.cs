using SchoolManagement_331.Repository.Repository;
using SchoolManagement_331.Repository.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace SchoolManagement_331
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();


            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserPanelInterfacecs, UserPanelServices>();
            container.RegisterType<ICountryInterface, CountryServices>();
            container.RegisterType<IStateInterface, StateServices>();
            container.RegisterType<ICityInterface, CityServices>();
            container.RegisterType<IFormDetailsInterface, FormDetailsServices>();
            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
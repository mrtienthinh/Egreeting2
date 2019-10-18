using Egreeting.Business.Business;
using Egreeting.Business.IBusiness;
using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using Egreeting.Repository.IRepository;
using Egreeting.Repository.Repository;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

namespace Egreeting.Web.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<DbContext, EgreetingContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>,UserStore<ApplicationUser>>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());


            // register all your components with the container here 
            // it is NOT necessary to register your controllers 

            // e.g. container.RegisterType<ITestService, TestService>(); 
            //container.RegisterType<ApplicationDbContext, ApplicationDbContext>(new PerRequestLifetimeManager());
            container.RegisterFactory<ILog>(x => LogManager.GetLogger("EGreetingLog"));
            // Repository layer
            container.RegisterType<IEcardRepository, EcardRepository>(new PerRequestLifetimeManager());
            container.RegisterType<ICategoryRepository, CategoryRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IEgreetingRoleRepository, EgreetingRoleRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IEgreetingUserRepository, EgreetingUserRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IFeedbackRepository, FeedbackRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IPaymentRepository, PaymentRepository>(new PerRequestLifetimeManager());
            container.RegisterType<ISubcriberRepository, SubcriberRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IScheduleSenderRepository, ScheduleSenderRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IOrderRepository, OrderRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IOrderDetailRepository, OrderDetailRepository>(new PerRequestLifetimeManager());


            // Business layer
            container.RegisterType<IEcardBusiness, EcardBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<ICategoryBusiness, CategoryBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IEgreetingRoleBusiness, EgreetingRoleBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IEgreetingUserBusiness, EgreetingUserBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IFeedbackBusiness, FeedbackBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IPaymentBusiness, PaymentBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<ISubcriberBusiness, SubcriberBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IScheduleSenderBusiness, ScheduleSenderBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IOrderBusiness, OrderBusiness>(new PerRequestLifetimeManager());
            container.RegisterType<IOrderDetailBusiness, OrderDetailBusiness>(new PerRequestLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
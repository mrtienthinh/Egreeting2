using Egreeting.Business.Business;
using Egreeting.Business.IBusiness;
using Egreeting.Models.AppContext;
using Egreeting.Repository.IRepository;
using Egreeting.Repository.Repository;
using log4net;
using System;
using System.Collections.Generic;
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
            //container.RegisterType<Controllers.Admin.AccountController>(new InjectionConstructor());
            container.RegisterType<EgreetingContext>(new InjectionConstructor());


            // register all your components with the container here 
            // it is NOT necessary to register your controllers 

            // e.g. container.RegisterType<ITestService, TestService>(); 
            //container.RegisterType<ApplicationDbContext, ApplicationDbContext>(new PerResolveLifetimeManager());
            container.RegisterFactory<ILog>(x => LogManager.GetLogger("EGreetingLog"));
            // Repository layer
            container.RegisterType<IEcardRepository, EcardRepository>(new PerResolveLifetimeManager());
            container.RegisterType<ICategoryRepository, CategoryRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IEgreetingRoleRepository, EgreetingRoleRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IEgreetingUserRepository, EgreetingUserRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IFeedbackRepository, FeedbackRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IPaymentRepository, PaymentRepository>(new PerResolveLifetimeManager());
            container.RegisterType<ISubcriberRepository, SubcriberRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IScheduleSenderRepository, ScheduleSenderRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IOrderRepository, OrderRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IOrderDetailRepository, OrderDetailRepository>(new PerResolveLifetimeManager());


            // Business layer
            container.RegisterType<IEcardBusiness, EcardBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<ICategoryBusiness, CategoryBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<IEgreetingRoleBusiness, EgreetingRoleBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<IEgreetingUserBusiness, EgreetingUserBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<IFeedbackBusiness, FeedbackBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<IPaymentBusiness, PaymentBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<ISubcriberBusiness, SubcriberBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<IScheduleSenderBusiness, ScheduleSenderBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<IOrderBusiness, OrderBusiness>(new PerResolveLifetimeManager());
            container.RegisterType<IOrderDetailBusiness, OrderDetailBusiness>(new PerResolveLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
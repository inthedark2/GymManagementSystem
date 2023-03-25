using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Builder;
using MS.DataLayer.Abstract;
using MS.DataLayer.Concrete;
using MS.DataLayer.Entities;
//using MS.WebSite;
using System.Web.Mvc;
using System.Reflection;

namespace MS.DI
{
    public class AutofacConfig
    {
        public static void ConfigureContainer(Assembly[] controllerAssemblies)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(controllerAssemblies);
            ManagmentSystemContext context = new ManagmentSystemContext();

            builder.RegisterType<CardRepository>().As<ICardRepository>().WithParameter("context", context);
            builder.RegisterType<ClientRepository>().As<IClientRepository>().WithParameter("context", context);
            builder.RegisterType<GenderRepository>().As<IGenderRepository>().WithParameter("context", context);
            builder.RegisterType<SubscriptionRepository>().As<ISubscriptionRepository>().WithParameter("context", context);
            builder.RegisterType<UserRepository>().As<IUserRepository>().WithParameter("context", context);
            builder.RegisterType<TrainingRepository>().As<ITrainingRepository>().WithParameter("context", context);
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>().WithParameter("context", context);
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
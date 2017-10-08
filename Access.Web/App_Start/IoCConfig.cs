using System.Web.Http;
using System.Web.Mvc;
using Access.API;
using Access.Data.DAL;
using Access.Data.Services;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Access.Web
{
	public class IoCConfig
	{
		public static void RegisterDependencies()
		{
			var builder = IoCApiConfig.RegisterApiDependencies();
			//var builder = new ContainerBuilder();

			//builder.RegisterAssemblyTypes()
			//       .Where(t => t.Name.EndsWith("Repository"))
			//       .AsImplementedInterfaces()
			//       .InstancePerRequest();
			//builder.RegisterAssemblyTypes()
			//       .Where(t => t.Name.EndsWith("Service"))
			//       .AsImplementedInterfaces()
			//       .InstancePerRequest();

			// Note that ASP.NET MVC requests controllers by their concrete types, 
			// so registering them As<IController>() is incorrect. 
			// Also, if you register controllers manually and choose to specify 
			// lifetimes, you must register them as InstancePerDependency() or 
			// InstancePerHttpRequest() - ASP.NET MVC will throw an exception if 
			// you try to reuse a controller instance for multiple requests. 

			builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();

			builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);

			/*
			 The MVC Integration includes an Autofac module that will add HTTP request 
			 lifetime scoped registrations for the HTTP abstraction classes. The 
			 following abstract classes are included: 
			-- HttpContextBase 
			-- HttpRequestBase 
			-- HttpResponseBase 
			-- HttpServerUtilityBase 
			-- HttpSessionStateBase 
			-- HttpApplicationStateBase 
			-- HttpBrowserCapabilitiesBase 
			-- HttpCachePolicyBase 
			-- VirtualPathProvider 

			To use these abstractions add the AutofacWebTypesModule to the container 
			using the standard RegisterModule method. 
			*/
			builder.RegisterModule<AutofacWebTypesModule>();

			builder.Register<IDbContext>(c => new ApplicationDbContext()).InstancePerLifetimeScope();
			builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

			builder.RegisterType<ClientService>().As<IClientService>().InstancePerLifetimeScope();
			builder.RegisterType<ProjectService>().As<IProjectService>().InstancePerLifetimeScope();
			builder.RegisterType<AccessService>().As<IAccessService>().InstancePerLifetimeScope();
			builder.RegisterType<AccessTypeService>().As<IAccessTypeService>().InstancePerLifetimeScope();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}
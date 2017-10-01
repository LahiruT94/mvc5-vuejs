using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;

namespace Access.API
{
	public class IoCApiConfig
	{
		public static ContainerBuilder RegisterApiDependencies()
		{
			var builder = new ContainerBuilder();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			return builder;
		}
	}
}
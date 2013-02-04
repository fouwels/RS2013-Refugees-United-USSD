using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;
using RS2013.RefugeesUnited.Data;
using RS2013.RefugeesUnited.Data.Impl;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Services;
using RS2013.RefugeesUnited.Services.Impl;
using Tam.Lib.Model;

namespace RS2013.RefugeesUnited.API
{
	public class MvcApplication : NinjectHttpApplication
	{
		protected override void OnApplicationStarted()
		{
			base.OnApplicationStarted();
			ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(Kernel));

			RouteTable.Routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional }
				);
		}

		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

			kernel.Bind<IDbContext>().To<ModelContainer>();

			kernel.Bind<IDbSet<Session>>().ToMethod(c => ((ModelContainer)c.Kernel.Get<IDbContext>()).Sessions);
			kernel.Bind<IDbSet<Device>>().ToMethod(c => ((ModelContainer)c.Kernel.Get<IDbContext>()).Devices);
			kernel.Bind<IDbSet<User>>().ToMethod(c => ((ModelContainer)c.Kernel.Get<IDbContext>()).Users);

			kernel.Bind<ISessionRepository>().To<SessionRepository>();
			kernel.Bind<IDeviceRepository>().To<DeviceRepository>();
			kernel.Bind<IUserRepository>().To<UserRepository>();

			kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
			kernel.Bind<ISessionService>().To<SessionService>();

			return kernel;
		}
	}

	public class NinjectControllerFactory : DefaultControllerFactory
	{
		private readonly IKernel _ninjectKernel;

		public NinjectControllerFactory(IKernel kernel)
		{
			_ninjectKernel = kernel;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			return (controllerType == null) ? null : (IController)_ninjectKernel.Get(controllerType);
		}
	}
}

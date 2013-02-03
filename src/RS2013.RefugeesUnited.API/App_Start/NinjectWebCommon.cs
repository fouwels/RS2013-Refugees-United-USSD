using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using RS2013.RefugeesUnited.API.App_Start;
using RS2013.RefugeesUnited.Data;
using RS2013.RefugeesUnited.Data.Impl;
using RS2013.RefugeesUnited.Model;
using RS2013.RefugeesUnited.Services;
using RS2013.RefugeesUnited.Services.Impl;
using Tam.Lib.Model;

[assembly: WebActivator.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]
namespace RS2013.RefugeesUnited.API.App_Start
{
	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			Bootstrapper.Initialize(CreateKernel);
		}

		public static void Stop()
		{
			Bootstrapper.ShutDown();
		}

		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

			kernel.Bind<IDbContext>().To<ModelContainer>();

			kernel.Bind<ISessionRepository>().To<SessionRepository>();
			kernel.Bind<IDeviceRepository>().To<DeviceRepository>();
			kernel.Bind<IUserRepository>().To<UserRepository>();

			kernel.Bind<ISessionService>().To<SessionService>();
			
			return kernel;
		}
	}
}

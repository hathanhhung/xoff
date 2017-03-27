using Autofac;
using XOFF.Core;
using XOFF.Core.Repositories;
using XOFF.Core.Settings;
using XOFF.LiteDB;
using XOFF.SQLite;

namespace XOFF.Autofac
{

	public class XOFFLiteDbAutoFacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<LiteDbConnectionProvider>().As<ILiteDbConnectionProvider>();
			builder.RegisterType<SyncRepositorySettings>().SingleInstance();
			builder.RegisterGeneric(typeof(LiteDBRepository<,>)).As(typeof(IRepository<,>));

			builder.RegisterModule<XOFFAutoFacBaseModule>();
		}
	}
    
}
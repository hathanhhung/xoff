﻿using System;
using System.IO;
using Autofac;
using Foundation;
using OfflineFirstReferenceArch.ViewModels;
using OfflineFirstReferenceArch.Widgets;
using OfflineFirstReferenceArch.Models;
using SQLite.Net;
using UIKit;
using XOFF.Autofac;
using XOFF.Core.Remote;
using LiteDB;

namespace OfflineFirstReferenceArch.IOS
{
    public static class DIContainer
    {
        public static IContainer ContainerInstance { get; set; }
    }


    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
       
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool WillFinishLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create our container builder
            var builder = new ContainerBuilder();


            RegisterDependencies(builder);
            DIContainer.ContainerInstance = builder.Build();

            // return true
            return true;
        }

        private void RegisterDependencies(ContainerBuilder builder)
        { 	//XOFF
			builder.RegisterType<RemoteWidgetGetter>().As<IRemoteEntityGetter<Widget, Guid>>();

			RegisterLiteDbDependencies(builder);

			//services, getters, etc. 
			builder.RegisterType<WidgetReader>().As<IWidgetReader>();
            builder.RegisterType<WidgetCreator>().As<IWidgetCreator>();

            //viewmodels
            builder.RegisterType<LandingPageViewModel>();
        }
		public void RegisterLiteDbDependencies(ContainerBuilder builder ) 
		{
			var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine(documentsPath, "..", "Library");
			var databasePath = Path.Combine(libraryPath, "widgets.liteDb");

			LiteDatabase database = new LiteDatabase(databasePath);
			builder.RegisterInstance(database).SingleInstance();

			builder.RegisterModule<XOFFLiteDbAutoFacModule>();
		}

		public void RegisterSQLiteDependencies(ContainerBuilder builder ) 
		{
			
			var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine(documentsPath, "..", "Library");
			var databasePath = Path.Combine(libraryPath, "widgets.sqlite");

			SQLiteConnection conn;
			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			conn = new SQLiteConnection(platform, databasePath, storeDateTimeAsTicks: false);
			builder.RegisterInstance(conn).SingleInstance();

			builder.RegisterModule<XOFFSQLiteAutoFacModule>();
		}

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {

			 Window = new UIWindow(UIScreen.MainScreen.Bounds);
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            var rootViewController = UIStoryboard.FromName("Main", null).InstantiateInitialViewController();
            Window.RootViewController = rootViewController;
            Window.MakeKeyAndVisible();

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}



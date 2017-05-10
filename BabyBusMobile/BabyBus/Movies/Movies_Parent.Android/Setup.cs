using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using BabyBus.Droid.Services;
using BabyBus.Droid.Utils;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Droid.Platform;
using CN.Jpush.Android.Api;
using Xamarin;
using BabyBus.Logic.Shared;

namespace BabyBus.Droid
{
	public class Setup : MvxAndroidSetup
	{
		public Setup(Context applicationContext)
			: base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp() {
			Insights.Initialize("a106ce8a29cf5c0aa929086ebd741f7d72730bd2", ApplicationContext);

			Mvx.LazyConstructAndRegisterSingleton<IPictureService, PictureService>();
			Mvx.LazyConstructAndRegisterSingleton<IEnvironmentService, EnvironmentService>();
//            Mvx.LazyConstructAndRegisterSingleton<ISqlitePlatformService, SqlitePlatformService>();
//            Mvx.LazyConstructAndRegisterSingleton<IMediaPicker,MediaPicker>();
			BaseViewModel.AppType = CustomConfig.ApkType;
			//Push Service
			JPushInterface.SetDebugMode(true);
			JPushInterface.Init(this.ApplicationContext);    

			return new App();
		}

		protected override IMvxTrace CreateDebugTrace() {
			return new MvxDebugTrace();
		}

		protected override IList<Assembly> AndroidViewAssemblies {
			get {
				return new List<Assembly>() {
					typeof(Android.Views.View).Assembly,
					typeof(Cirrious.MvvmCross.Binding.Droid.Views.MvxDatePicker).Assembly,
					this.GetType().Assembly
				};
			}
		}
	}
}
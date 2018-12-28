using GalaSoft.MvvmLight.Ioc;
using WeatherGuesser.Interfaces;
using WeatherGuesser.Loggers;
using WeatherGuesser.Services;

namespace WeatherGuesserApp.ViewModel
{
	public class ViewModelLocator
	{

		public HomeViewModel HomeViewModel => SimpleIoc.Default.GetInstance<HomeViewModel>();

		public ViewModelLocator()
		{
			SimpleIoc.Default.Register<ILearningService, MulticlassSvmLearningService>();
			SimpleIoc.Default.Register<INormalizeService, StandardNormalizeService>();
			SimpleIoc.Default.Register<ILogger, DebugLogger>();

			SimpleIoc.Default.Register<HomeViewModel>();
		}

	}
}

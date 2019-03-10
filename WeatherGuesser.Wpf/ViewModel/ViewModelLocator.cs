using GalaSoft.MvvmLight.Ioc;
using WeatherGuesser.Model.Interfaces;
using WeatherGuesser.Model.Loggers;
using WeatherGuesser.Model.Services;

namespace WeatherGuesser.Wpf.ViewModel
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

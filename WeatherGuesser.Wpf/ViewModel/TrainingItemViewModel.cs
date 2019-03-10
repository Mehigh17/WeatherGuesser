using GalaSoft.MvvmLight;
using WeatherGuesser.Model.Enums;

namespace WeatherGuesser.Wpf.ViewModel
{
    public class TrainingItemViewModel : ViewModelBase
	{
		private WeatherType _weatherType;
		private string _name;
		private bool _isChecked;

		public WeatherType WeatherType
		{
			get => _weatherType;
			set => Set(ref _weatherType, value);
		}

		public string Name
		{
			get => _name;
			set => Set(ref _name, value);
		}

		public bool IsChecked
		{
			get => _isChecked;
			set => Set(ref _isChecked, value);
		}
	}
}

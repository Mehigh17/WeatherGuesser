using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeatherGuesser.Enums;
using WeatherGuesser.Interfaces;
using WeatherGuesserApp.Properties;
using RelayCommand = GalaSoft.MvvmLight.Command.RelayCommand;

namespace WeatherGuesserApp.ViewModel
{
	public class HomeViewModel : ViewModelBase
	{

		private readonly ILearningService _learningService;
		private readonly INormalizeService _normalizeService;

		public HomeViewModel(ILearningService learningService, INormalizeService normalizeService)
		{
			_learningService = learningService;
			_normalizeService = normalizeService;
		}

		private int _modelCount;
		public int ModelCount
		{
			get => _modelCount;
			set => Set(ref _modelCount, value);
		}

		private bool _isTraining;
		public bool IsTraining
		{
			get => _isTraining;
			set => Set(ref _isTraining, value);
		}

		private string _displayedEvaluationImage;
		public string DisplayedEvaluationImage
		{
			get => _displayedEvaluationImage;
			set => Set(ref _displayedEvaluationImage, value);
		}

		public ObservableCollection<KeyValuePair<string, string>> DataPaths { get; set; } = new ObservableCollection<KeyValuePair<string, string>>();

		private RelayCommand _openTrainingDataBrowserCommand;
		public RelayCommand OpenTrainingDataBrowserCommand
		{
			get
			{
				return _openTrainingDataBrowserCommand ?? (_openTrainingDataBrowserCommand = new RelayCommand(() =>
				{
					var browserDialog = new FolderBrowserDialog
					{
						Description = Resources.TrainingDataBrowserTip,
						ShowNewFolderButton = true
					};
					browserDialog.ShowDialog();

					if(string.IsNullOrEmpty(browserDialog.SelectedPath))
						return;

					var weatherNames = Enum.GetNames(typeof(WeatherType)).Select(n => n.ToLower());

					DataPaths.Clear();
					foreach (var weatherName in weatherNames)
					{
						var filesPath = Path.Combine(browserDialog.SelectedPath, weatherName);
						if (Directory.Exists(filesPath))
						{
							foreach (var filePath in Directory.GetFiles(filesPath))
							{
								DataPaths.Add(new KeyValuePair<string, string>(weatherName, Path.GetFileName(filePath)));
							}
						}
						else
						{
							MessageBox.Show(string.Format(Resources.DirectoryNotFoundWarning, weatherName),
								Resources.DirectoryNotFoundTitle);
							break;
						}
					}

					ModelCount = DataPaths.Count;
				}, () => !IsTraining));
			}
		}

		private RelayCommand _selectEvaluationImageCommand;
		public RelayCommand SelectEvaluationImageCommand
		{
			get
			{
				return _selectEvaluationImageCommand ??
				       (_selectEvaluationImageCommand = new RelayCommand(() =>
				       {
					       var fileBrowser = new OpenFileDialog
					       {
						       Multiselect = false,
							   CheckFileExists = true,
							   CheckPathExists = true,
					       };
					       fileBrowser.ShowDialog();

					       DisplayedEvaluationImage = fileBrowser.FileName;
				       }));
			}
		}

		private RelayCommand _startTrainingCommand;
		public RelayCommand StartTrainingCommand
		{
			get
			{
				return _startTrainingCommand ??
				       (_startTrainingCommand = new RelayCommand(() =>
				       {
						   IsTraining = true;
					   }, () => ModelCount > 0 && !IsTraining));
			}
		}
	}
}

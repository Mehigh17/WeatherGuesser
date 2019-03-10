using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using WeatherGuesser.Model.Enums;
using WeatherGuesser.Model.Interfaces;
using WeatherGuesser.Wpf.Properties;
using RelayCommand = GalaSoft.MvvmLight.CommandWpf.RelayCommand;

namespace WeatherGuesser.Wpf.ViewModel
{
    public class HomeViewModel : ViewModelBase
	{

		private readonly ILearningService _learningService;
		private readonly INormalizeService _normalizeService;
		private readonly Dictionary<TrainingItemViewModel, string> _trainingItems;

		public HomeViewModel(ILearningService learningService, INormalizeService normalizeService)
		{
			_learningService = learningService;
			_normalizeService = normalizeService;
			_trainingItems = new Dictionary<TrainingItemViewModel, string>();

			StatusText = "Idling";
		}

		private bool _isSvmReady;
		public bool IsSvmReady
		{
			get => _isSvmReady;
			set => Set(ref _isSvmReady, value);
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

		private bool _isTrained;
		public bool IsTrained
		{
			get => _isTrained;
			set => Set(ref _isTrained, value);
		}

		private string _displayedEvaluationImage;
		public string DisplayedEvaluationImage
		{
			get => _displayedEvaluationImage;
			set => Set(ref _displayedEvaluationImage, value);
		}

		private string _statusText;
		public string StatusText
		{
			get => _statusText;
			set => Set(ref _statusText, value);
		}

		public ObservableCollection<TrainingItemViewModel> DataPaths { get; set; } =
			new ObservableCollection<TrainingItemViewModel>();

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

					if (string.IsNullOrEmpty(browserDialog.SelectedPath))
						return;

					var weatherNames = Enum.GetNames(typeof(WeatherType)).Select(n => n.ToLower());

					DataPaths.Clear();
					_trainingItems.Clear();
					foreach (var weatherName in weatherNames)
					{
						var filesPath = Path.Combine(browserDialog.SelectedPath, weatherName);
						if (Directory.Exists(filesPath))
						{
							var files = Directory.GetFiles(filesPath);

							foreach (var filePath in files)
							{
								var trainingItem = new TrainingItemViewModel
								{
									WeatherType = (WeatherType)Enum.Parse(typeof(WeatherType), weatherName, true),
									Name = Path.GetFileName(filePath),
									IsChecked = false
								};
								_trainingItems.Add(trainingItem, filePath);
								DataPaths.Add(trainingItem);
							}
						}
						else
						{
							MessageBox.Show(string.Format(Resources.DirectoryNotFoundWarning, weatherName),
								Resources.DirectoryNotFoundTitle);
							break;
						}
					}

					ModelCount = _trainingItems.Count;

					StatusText = Resources.ReadyToTrain;
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
						   var filePath = fileBrowser.FileName;

						   if (File.Exists(filePath))
						   {
							   DisplayedEvaluationImage = filePath;
							   var result = _learningService.GetResult(filePath);

							   StatusText = $"Answer: {result.ToString()}";
						   }
					   }, () => IsSvmReady));
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
						   var worker = new BackgroundWorker()
						   {
							   WorkerReportsProgress = false
						   };
						   worker.DoWork += (sender, args) =>
						   {
							   StatusText = Resources.NormalizingImages;

							   var normalizedImages = new List<KeyValuePair<WeatherType, double[]>>();
							   foreach (var (itemViewModel, dataPath) in _trainingItems.Select(x => (x.Key, x.Value)))
							   {
								   var image = Image.FromFile(dataPath);
								   var normalizedImage = _normalizeService.GetImage(image);
								   normalizedImages.Add(new KeyValuePair<WeatherType, double[]>(itemViewModel.WeatherType, normalizedImage));

								   itemViewModel.IsChecked = true;
							   }

							   StatusText = Resources.Training;
							   _learningService.Learn(normalizedImages);

							   IsSvmReady = true;
							   IsTraining = false;
							   IsTrained = true;

							   StatusText = Resources.TrainingComplete;
						   };
						   worker.RunWorkerCompleted += (sender, args) => IsTraining = false;
						   worker.RunWorkerAsync();
					   }, () => ModelCount > 0 && !IsTraining));
			}
		}

		private RelayCommand _loadMachineCommand;
		public RelayCommand LoadMachineCommand
		{
			get
			{
				return _loadMachineCommand ?? (_loadMachineCommand = new RelayCommand(() =>
						   {
							   var browserDialog = new OpenFileDialog()
							   {
								   Multiselect = false,
								   Filter = "Data files|*.data",
								   AddExtension = true
							   };

							   if (browserDialog.ShowDialog() == DialogResult.OK)
							   {
								   var selectedFile = browserDialog.FileName;
								   if (File.Exists(selectedFile))
								   {
									   _learningService.Load(selectedFile);
									   StatusText = Resources.LoadSuccessfull;

									   IsSvmReady = true;
									   IsTrained = true;
								   }
							   }
						   }, () => !IsTraining));
			}
		}

		private RelayCommand _saveMachineCommand;
		public RelayCommand SaveMachineCommand
		{
			get
			{
				return _saveMachineCommand ?? (_saveMachineCommand = new RelayCommand(() =>
				{
					var browserDialog = new SaveFileDialog()
					{
						AddExtension = true,
						CheckPathExists = true,
						FileName = "machine.data",
						DefaultExt = ".data",
					};

					if (browserDialog.ShowDialog() == DialogResult.OK)
					{
						_learningService.Save(browserDialog.FileName);
					}
				}, () => !IsTraining && IsTrained && IsSvmReady));
			}
		}
	}
}

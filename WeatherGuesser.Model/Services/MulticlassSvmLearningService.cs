using Accord.IO;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WeatherGuesser.Model.Enums;
using WeatherGuesser.Model.Interfaces;

namespace WeatherGuesser.Model.Services
{
    public class MulticlassSvmLearningService : ILearningService
	{

		private readonly INormalizeService _normalizeService;
		private MulticlassSupportVectorMachine<Linear> _msvMachine;

		public MulticlassSvmLearningService(INormalizeService normalizeService)
		{
			_normalizeService = normalizeService;
		}

		public WeatherType GetResult(double[] image)
		{
			if (_msvMachine == null)
				throw new InvalidOperationException(
					"The MulticlassSupportVectorMachine hasn't been initialized. Please make it learn or load it first.");

			return (WeatherType)_msvMachine.Decide(image);
		}

		public WeatherType GetResult(string imagePath)
		{
			return GetResult(Image.FromFile(imagePath));
		}

		public WeatherType GetResult(Image image)
		{
			return GetResult(_normalizeService.GetImage(image));
		}

		public void Learn(List<KeyValuePair<WeatherType, double[]>> data)
		{
			var teacher = new MulticlassSupportVectorLearning<Linear>()
			{
				Learner = (p) => new SequentialMinimalOptimization<Linear>()
				{
					Complexity = 10000.0
				}
			};

			var inputs = data.Select(d => d.Value).ToArray();
			var outputs = data.Select(d => (int) d.Key).ToArray();
			_msvMachine = teacher.Learn(inputs, outputs);
		}

		public void Save(string path)
		{
			_msvMachine.Save(path);
		}

		public void Load(string path)
		{
			_msvMachine = Serializer.Load<MulticlassSupportVectorMachine<Linear>>(path);
		}

	}
}

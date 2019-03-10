using System.Collections.Generic;
using System.Drawing;
using WeatherGuesser.Model.Enums;

namespace WeatherGuesser.Model.Interfaces
{
	public interface ILearningService
	{

		WeatherType GetResult(double[] image);
		WeatherType GetResult(string imagePath);
		WeatherType GetResult(Image image);

		void Learn(List<KeyValuePair<WeatherType, double[]>> data);

		void Load(string path);
		void Save(string path);

	}
}

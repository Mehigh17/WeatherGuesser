using System;
using System.Diagnostics;
using System.Text;
using WeatherGuesser.Model.Interfaces;

namespace WeatherGuesser.Model.Loggers
{
	public class DebugLogger : ILogger
	{

		public bool ShowTimestamp { get; set; } = true;

		public void Log(string message)
		{
			var stringBuilder = new StringBuilder();

			if (ShowTimestamp)
			{
				stringBuilder.Append($"[{DateTime.Now.ToShortTimeString()}]");
			}

			stringBuilder.Append(message);

			Debug.WriteLine(stringBuilder.ToString());
		}
	}
}

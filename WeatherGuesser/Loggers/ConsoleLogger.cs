using System;
using System.Text;
using WeatherGuesser.Interfaces;

namespace WeatherGuesser.Loggers
{
	public class ConsoleLogger : ILogger
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
		}
	}
}

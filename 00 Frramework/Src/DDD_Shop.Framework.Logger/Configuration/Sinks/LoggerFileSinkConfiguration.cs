using DDD_Shop.Framework.Logger.Definitions;

namespace DDD_Shop.Framework.Logger.Configuration.Sinks
{
	public class LoggerFileSinkConfiguration
	{
		public EtraabLogLevel Level { get; set; }
		public EtraabRollingInterval RollingInterval { get; set; }
		public int Limit { get; set; }

		public LoggerFileSinkConfiguration(EtraabLogLevel level = EtraabLogLevel.Debug,
			EtraabRollingInterval rollingInterval = EtraabRollingInterval.Day,
			int limit = 10)
		{
			Level = level;
			RollingInterval = rollingInterval;
			Limit = limit;
		}
	}
}
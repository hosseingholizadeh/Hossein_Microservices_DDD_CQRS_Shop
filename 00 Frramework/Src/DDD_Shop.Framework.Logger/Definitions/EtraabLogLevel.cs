using Serilog.Events;

namespace DDD_Shop.Framework.Logger.Definitions
{
	/// <summary>
	/// Same as Srilog
	/// </summary>
	public enum EtraabLogLevel
	{
		Verbose,
		Debug,
		Information,
		Warning,
		Error,
		Fatal
	}

	public static class EtraabLogLevelExtensions
	{
		public static LogEventLevel ResolveForSerilog(this EtraabLogLevel level)
		{
			switch (level)
			{
				case EtraabLogLevel.Verbose:
					return LogEventLevel.Verbose;
				case EtraabLogLevel.Debug:
					return LogEventLevel.Debug;
				case EtraabLogLevel.Information:
					return LogEventLevel.Information;
				case EtraabLogLevel.Warning:
					return LogEventLevel.Warning;
				case EtraabLogLevel.Error:
					return LogEventLevel.Error;
				default: return LogEventLevel.Information;
			}
		}
	}
}

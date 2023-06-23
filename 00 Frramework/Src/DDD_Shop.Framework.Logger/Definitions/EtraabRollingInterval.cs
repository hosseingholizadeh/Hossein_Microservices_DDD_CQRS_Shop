using Serilog;

namespace DDD_Shop.Framework.Logger.Definitions
{
	/// <summary>
	/// Same as Srilog
	/// </summary>
	public enum EtraabRollingInterval
	{
		Infinite,
		Year,
		Month,
		Day,
		Hour,
		Minute
	}

	public static class EtraabRollingIntervalExtensions
	{
		public static RollingInterval ResolveForSerilog(this EtraabRollingInterval interval)
		{
			switch (interval)
			{
				case EtraabRollingInterval.Infinite:
					return RollingInterval.Infinite;
				case EtraabRollingInterval.Year:
					return RollingInterval.Year;
				case EtraabRollingInterval.Month:
					return RollingInterval.Month;
				case EtraabRollingInterval.Day:
					return RollingInterval.Day;
				case EtraabRollingInterval.Hour:
					return RollingInterval.Hour;
				case EtraabRollingInterval.Minute:
					return RollingInterval.Minute;
				default: return RollingInterval.Day;
			}
		}
	}
}

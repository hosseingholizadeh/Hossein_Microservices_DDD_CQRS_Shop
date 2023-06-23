using DDD_Shop.Framework.Utils.Exceptions;
using System.Security;

namespace DDD_Shop.Framework.Utils;

public class EnvironmentKeeper
{
	public const char CLUSTER_SPERATOR = ',';

	public const string REDIS_CONNS = "REDIS_CONNS";

	public const string BROKER_BOOTSTRAPER_SERVERS = "BROKER_BOOTSTRAPER_SERVERS";

	public const string FILE_SOUNDS = "FILE_SOUNDS";

	public const string LOG_URI_ELK = "LOG_URI_ELK";
	public const string LOG_LEVEL_ELK = "LOG_LEVEL_ELK";

	public const string LOG_GLOBAL_LEVEL = "LOG_GLOBAL_LEVEL";
	public const string LOG_FILE = "LOG_FILE";
	public const string LOG_LEVEL_FILE = "LOG_LEVEL_FILE";
	public const string LOG_LIMIT_FILE = "LIG_LIMIT_FILE";
	public const string LOG_ROLLING_INTERVAL_FILE = "LOG_ROLLING_INTERVAL_FILE";

	public static string ReadVariable(string name)
	{
		try
		{
			return Environment.GetEnvironmentVariable(name);
		}
		catch (ArgumentNullException)
		{
			return null;
		}
		catch (SecurityException)
		{
			return null;
		}
	}

	public static T? ReadVariable<T>(string name)
	{
		var value = ReadVariable(name);
		if (value == null) return default;

		return (T)Convert.ChangeType(value, typeof(T));
	}

	public static string ReadRequiredVariable(string name)
	{
		try
		{
			var value = Environment.GetEnvironmentVariable(name);
			if (value == null) throw new ArgumentNullException();
			return value;
		}
		catch (ArgumentNullException)
		{
			throw new EnviromentVariableNotFoundException($"Environment Variable {name} Not Provided");
		}
		catch (SecurityException)
		{
			throw new EnviromentVariableNotFoundException($"SecurityException - Environment Variable {name} Not Found");
		}
	}

	public static T ReadRequiredVariable<T>(string name)
	{
		return (T)Convert.ChangeType(ReadRequiredVariable(name), typeof(T));
	}

	public static bool IsFromSettingFile()
	{
		try
		{
			return Environment.GetEnvironmentVariable("FromFile") == "true";
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static bool IsTestEnvironment()
	{
		try
		{
			return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test";
		}
		catch (Exception)
		{
			return false;
		}
	}
}

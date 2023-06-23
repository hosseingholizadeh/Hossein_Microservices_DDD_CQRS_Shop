using DDD_Shop.Framework.Logger.Definitions;

namespace DDD_Shop.Framework.Logger.Configuration.Sinks
{
	public class LoggerElasticSinkConfiguration
	{
		public string Uri { get; set; }
		public EtraabLogLevel Level { get; set; }

		public LoggerElasticSinkConfiguration(string uri,
			EtraabLogLevel level = EtraabLogLevel.Debug)
		{
			if (string.IsNullOrWhiteSpace(uri))
				throw new ArgumentNullException(nameof(uri));

			Uri = uri;
			Level = level;
		}
	}
}
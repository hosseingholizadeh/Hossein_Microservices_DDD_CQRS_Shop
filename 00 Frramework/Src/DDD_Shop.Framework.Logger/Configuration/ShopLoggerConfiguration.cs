using DDD_Shop.Framework.Logger.Configuration.Sinks;
using DDD_Shop.Framework.Logger.Definitions;
using DDD_Shop.Framework.Utils;
using DDD_Shop.Framework.Utils.Exceptions;
using DDD_Shop.Framework.Utils.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DDD_Shop.Framework.Logger.Configuration
{
	public class ShopLoggerConfiguration
	{
		public const string SECTION_NAME = "Log";

		public EtraabLogLevel? GlobalLevel { get; set; }
		public LoggerElasticSinkConfiguration? Elastic { get; set; }
		public LoggerFileSinkConfiguration? File { get; set; }

		private void SetElasticeSinkConfiguration()
		{
			var uri = EnvironmentKeeper.ReadVariable(EnvironmentKeeper.LOG_URI_ELK);

			if (string.IsNullOrWhiteSpace(uri))
				return;

			var level = EnvironmentKeeper.ReadVariable<short>(EnvironmentKeeper.LOG_LEVEL_ELK);
			Elastic = new LoggerElasticSinkConfiguration(uri, (EtraabLogLevel)level);
		}

		private void SetFileSinkConfiguration()
		{
			var file = EnvironmentKeeper.ReadVariable(EnvironmentKeeper.LOG_URI_ELK);
			if (string.IsNullOrWhiteSpace(file))
				return;

			var level = EnvironmentKeeper.ReadVariable<short?>(EnvironmentKeeper.LOG_LEVEL_FILE);
			var limit = EnvironmentKeeper.ReadVariable<int>(EnvironmentKeeper.LOG_LIMIT_FILE);
			var rollingInterval = EnvironmentKeeper.ReadVariable<short>(EnvironmentKeeper.LOG_ROLLING_INTERVAL_FILE);

			File = new LoggerFileSinkConfiguration(level != null ? (EtraabLogLevel)level
				 : GlobalLevel != null ? GlobalLevel.Value : EtraabLogLevel.Information,
				 (EtraabRollingInterval)rollingInterval, limit);
		}

		public static ShopLoggerConfiguration Config;

		public static ShopLoggerConfiguration FromEnviroment()
		{
			var config = new ShopLoggerConfiguration();

			var globalLevel = EnvironmentKeeper.ReadVariable<short?>(EnvironmentKeeper.LOG_GLOBAL_LEVEL);
			if (globalLevel != null)
				config.GlobalLevel = (EtraabLogLevel)globalLevel;

			config.SetFileSinkConfiguration();
			config.SetElasticeSinkConfiguration();

			return config;
		}

		public static ShopLoggerConfiguration FromConfiguration(IConfiguration configuration)
		{
			IConfigurationSection section;
			try
			{
				section = configuration.GetRequiredSection(SECTION_NAME);
			}
			catch (InvalidOperationException)
			{
				throw new ConfigurationInvalidDataException(SECTION_NAME);
			}

			try
			{
				var config = new ShopLoggerConfiguration();
				new ConfigureFromConfigurationOptions<ShopLoggerConfiguration>(section)
				   .Configure(config);
				return config;
			}
			catch (Exception)
			{
				throw new ConfigurationInvalidDataException(SECTION_NAME);
			}
		}

		public static void Configure(IConfiguration? configuration = null)
		{
			Config = configuration == null ? FromEnviroment() : FromConfiguration(configuration);
		}

		public override string ToString()
		{
			return this.ToJson();
		}
	}
}

using DDD_Shop.Framework.Logger.Configuration;
using DDD_Shop.Framework.Logger.Definitions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace DDD_Shop.Framework.Logger.Extensions
{
	public static class LoggerRegisterationExtension
	{
		public static void UseShopLogger(this IHostBuilder builder,
			IConfiguration? configuration = null)
		{
			ShopLoggerConfiguration.Configure(configuration);
			builder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ConfigureLogger());
		}

		private static void ConfigureLogger(this LoggerConfiguration loggerConfiguration)
		{
			var config = ShopLoggerConfiguration.Config;
			loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
			.Enrich.FromLogContext()
			.WriteTo.Debug();

			if (config.File != null)
			{
				loggerConfiguration.WriteTo.File("logs/log.log",
						rollingInterval: config.File.RollingInterval.ResolveForSerilog(),
						restrictedToMinimumLevel: config.File.Level.ResolveForSerilog());
			}

			if (config.Elastic != null)
			{
				var level = config.Elastic.Level.ResolveForSerilog();
				loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config.Elastic.Uri))
				{
					AutoRegisterTemplate = true,
					MinimumLogEventLevel = level,
					AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
					IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
				});
			}

			loggerConfiguration.WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug);
		}
	}
}

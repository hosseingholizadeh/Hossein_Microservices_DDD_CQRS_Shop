using System.Net;

namespace DDD_Shop.Framework.Utils.Exceptions
{
	public class ConfigurationInvalidDataException : BaseException
	{
		public ConfigurationInvalidDataException(string config)
			: base($"{config} Invalid", $"{config} has invalid data structure", HttpStatusCode.InternalServerError)
		{
		}
	}
}

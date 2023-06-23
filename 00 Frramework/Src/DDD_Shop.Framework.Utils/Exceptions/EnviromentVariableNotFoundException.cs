using System.Net;

namespace DDD_Shop.Framework.Utils.Exceptions
{
	public class EnviromentVariableNotFoundException : BaseException
	{
		public EnviromentVariableNotFoundException(string variable)
			: base($"{variable} Not Found", $"{variable} not provided from the enviroment variables", HttpStatusCode.NotFound)
		{
		}
	}
}

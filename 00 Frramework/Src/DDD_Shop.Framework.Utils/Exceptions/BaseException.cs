using System.Net;

namespace DDD_Shop.Framework.Utils.Exceptions
{
	public class BaseException : Exception
	{
		public BaseException(string type, string detail, HttpStatusCode statusCode = HttpStatusCode.BadRequest) :
			this(type, detail, null, null, statusCode)
		{
		}

		public BaseException(string type, string detail, string entityName, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : this(type, detail, entityName, null, statusCode)
		{
		}

		public BaseException(string type, string detail, string entityName, string errorKey, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(detail)
		{
			Type = type;
			Detail = detail;
			EntityName = entityName;
			ErrorKey = errorKey;
			StatusCode = statusCode;
		}

		public HttpStatusCode StatusCode { get; set; }
		public string Type { get; set; }
		public string Detail { get; set; }
		public string EntityName { get; set; }
		public string ErrorKey { get; set; }
	}
}

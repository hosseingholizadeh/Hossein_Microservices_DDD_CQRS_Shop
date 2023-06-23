using DDD_Shop.Framework.Utils.Extensions;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DDD_Shop.Framework.Utils.Extensions
{
	public static class StringExtension
	{
		public static string ToJson<T>(this T obj)
		{
			var json = new JsonSerializer();
			using (var w = new StringWriter())
			{
				json.NullValueHandling = NullValueHandling.Ignore;
				json.Serialize(w, obj, typeof(T));
				return w.ToString();
			}
		}

		public static string ToJson<T>(this List<T> list)
		{
			var json = new JsonSerializer();
			using (var w = new StringWriter())
			{
				json.TypeNameHandling = TypeNameHandling.Objects;
				json.Serialize(w, list, typeof(T));
				return w.ToString();
			}
		}

		public static string ToJson<T>(this T obj, bool ignoreNulls)
		{
			var json = new JsonSerializer();
			using (var w = new StringWriter())
			{
				if (ignoreNulls)
					json.NullValueHandling = NullValueHandling.Ignore;
				json.Serialize(w, obj, typeof(T));
				return w.ToString();
			}
		}

		public static string ToPrettyJson(this object obj)
		{
			var json = new JsonSerializer()
			{
				Formatting = Newtonsoft.Json.Formatting.Indented
			};

			using (var w = new StringWriter())
			{
				json.Serialize(w, obj);
				return w.ToString();
			}
		}

		public static string ToJson<T>(this T obj, JsonSerializerSettings settings)
		{
			return JsonConvert.SerializeObject(obj, typeof(T), settings);
		}

		public static string ToJson<T>(this T obj, int maxDepth)
		{
			var json = new JsonSerializer();
			using (var w = new StringWriter())
			{
				json.NullValueHandling = NullValueHandling.Ignore;
				json.MaxDepth = maxDepth;
				json.Serialize(w, obj, typeof(T));
				return w.ToString();
			}
		}

		public static T FromJson<T>(this string str)
		{
			var json = new JsonSerializer();
			using (var r = new StringReader(str))
			using (var j = new JsonTextReader(r))
			{
				json.TypeNameHandling = TypeNameHandling.All;
				return json.Deserialize<T>(j);
			}
		}

		public static string ToJsonAbstract<T>(this T obj)
		{
			JsonSerializer serializer = new JsonSerializer();
			serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
			serializer.NullValueHandling = NullValueHandling.Ignore;
			serializer.TypeNameHandling = TypeNameHandling.Auto;
			serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

			using (var writer = new StringWriter())
			{
				serializer.Serialize(writer, obj, typeof(T));
				return writer.ToString();
			}
		}

		public static T FromJsonAbstract<T>(this string str)
		{
			var json = new JsonSerializer();
			using (var r = new StringReader(str))
			using (var j = new JsonTextReader(r))
			{
				return JsonConvert.DeserializeObject<T>(str, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Auto,
					NullValueHandling = NullValueHandling.Ignore,
				});
			}
		}

		public static byte[] ToUtfBytes(this string str)
		{
			return Encoding.UTF8.GetBytes(str);
		}

		public static T FromXml<T>(this string input) where T : class
		{
			XmlSerializer ser = new XmlSerializer(typeof(T));
			using (StringReader sr = new StringReader(input))
			{
				return (T)ser.Deserialize(sr);
			}
		}

		public static string ToXml<T>(this T value)
		{
			var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
			var serializer = new XmlSerializer(value.GetType());
			var settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;

			using (var stream = new StringWriter())
			using (var writer = XmlWriter.Create(stream, settings))
			{
				serializer.Serialize(writer, value, emptyNamespaces);
				return stream.ToString();
			}
		}

		public static bool HasNonAsciiChars(this string str)
		{
			const int MaxAnsiCode = 255;
			return str.Any(c => c > MaxAnsiCode);
		}

		public static string CapitalizeFirstLetter(this string input)
		{
			return input switch
			{
				null or "" => input,
				_ => input[0].ToString().ToUpper() + input.Substring(1),
			};
		}

		public static string ToLowerFirstLetter(this string input)
		{
			return input switch
			{
				null or "" => input,
				_ => input[0].ToString().ToLower() + input.Substring(1),
			};
		}

		public static List<string> ToList(this string input, char seperator = ',')
		{
			if (string.IsNullOrWhiteSpace(input)) return new List<string>(0);
			return input.Split(seperator).ToList();
		}

		public static int UniqueCharsCount(this string input)
		{
			var chars = input.ToArray();
			var uniqueChars = chars.Distinct();
			var count = uniqueChars.Where(x => chars.Where(c => c == x).Count() == 1).Count();
			return count;
		}

	}
}

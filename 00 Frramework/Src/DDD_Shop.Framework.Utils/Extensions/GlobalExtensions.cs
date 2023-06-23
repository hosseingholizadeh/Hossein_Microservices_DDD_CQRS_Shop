using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DDD_Shop.Framework.Utils.Extensions
{
	public static class GlobalExtensions
	{
		public static string GetDescription(this Enum value)
		{
			var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
			var descriptionAttribute =
				enumMember == null
					? default
					: enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
			return
				descriptionAttribute == null
					? value.ToString()
					: descriptionAttribute.Description;
		}

		public static string GetDisplayName(this Enum value)
		{
			var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
			var descriptionAttribute =
				enumMember == null
					? default
					: enumMember.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
			return
				descriptionAttribute == null
					? value.ToString()
					: descriptionAttribute.Name;
		}

		public static bool HasGuidValue(this Guid value) => value != Guid.Empty;
		public static bool HasGuidValue(this Guid? value) => value != null || value != Guid.Empty;
	}
}

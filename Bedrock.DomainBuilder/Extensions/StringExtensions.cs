using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bedrock.DomainBuilder
{
    public static class StringExtensions
    {
        #region Extension Methods
        public static List<string> ToList(this string value, string delimeter)
        {
            var list = value.Split(delimeter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(list);
        }

        public static string LowerCaseFirstCharacter(this string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("value");

            return value.First()
                        .ToString()
                        .ToLower() + value.Substring(1);
        }

        public static string AddSpacesBeforeCapitals(this string value)
        {
            return Regex
                    .Replace(value, "[A-Z]", " $0")
                    .TrimStart(new char[] { ' ' });
        }

        public static string ToSeparatedWords(this string value)
        {
            if (value != null && value.Length > 3)
                return Regex.Replace(value, "([A-Z][a-z]?)", " $1").Trim();

            return value;
        }

        public static string ToCamelCase(this string value)
        {
            var builder = new StringBuilder();
            var values = value.ToSeparatedWords().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            values.Each(v =>
            {
                builder.Append(v.First().ToString().ToUpper() + v.Substring(1).ToLower());
            });

            return builder.ToString();
        }
        #endregion
    }
}

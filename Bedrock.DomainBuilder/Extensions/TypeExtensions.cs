using System;
using System.Linq;

namespace Bedrock.DomainBuilder
{
    public static class TypeExtensions
    {
        #region Public Methods
        public static bool IsNumericType(this Type type)
        {
            return Enum
                    .GetNames(typeof(eNumericType))
                    .Any(n => n == type.Name);
        }

        public static object GetDefaultValue(this Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
        #endregion
    }
}

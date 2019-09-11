using System;

namespace Bedrock.DomainBuilder
{
    public static class TypeSwitch
    {
        #region Public Methods
        public static void On<T>(params CaseInfo[] cases)
        {
            var type = typeof(T);

            foreach (var entry in cases)
            {
                if (entry.IsDefault || entry.Target.IsAssignableFrom(type))
                {
                    entry.Action();
                    break;
                }
            }
        }

        public static CaseInfo Case<T>(Action action)
        {
            return new CaseInfo()
            {
                Action = action,
                Target = typeof(T)
            };
        }

        public static CaseInfo Default(Action action)
        {
            return new CaseInfo()
            {
                Action = action,
                IsDefault = true
            };
        }
        #endregion

        #region Classes
        public class CaseInfo
        {
            public Type Target { get; set; }
            public Action Action { get; set; }
            public bool IsDefault { get; set; }
        }
        #endregion
    }
}

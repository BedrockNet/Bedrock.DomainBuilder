using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Bedrock.DomainBuilder
{
    public static class IOHelper
    {
        #region Public Methods
        public static string GetExecutionPathLocation(params object[] args)
        {
            var stringArgs = new List<object>(args);
            var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            stringArgs.Insert(0, executionPath);

            return string.Concat(stringArgs);
        }

        public static void EnsurePath(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            finally { }
        }

        public static void CleanPath(string path, BuildSettings settings)
        {
            try
            {
                if (settings.IsCleanPath)
                {
                    var pathInfo = new DirectoryInfo(path);

                    foreach (var file in pathInfo.GetFiles())
                        file.Delete();
                }
            }
            finally { }
        }

        public static void WriteFile(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
        #endregion
    }
}


using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder
{
    public class BuildWarning
    {
        #region Constructors
        private BuildWarning() { }
        #endregion

        #region Properties
        public eBuilder Builder { get; set; }

        public string Section { get; set; }

        public string Message { get; set; }
        #endregion

        #region Public Methods
        public static BuildWarning Create()
        {
            return new BuildWarning();
        }

        public static BuildWarning Create(eBuilder builder, string section, string message)
        {
            return new BuildWarning
            {
                Builder = builder,
                Section = section,
                Message = message
            };
        }
        #endregion
    }
}

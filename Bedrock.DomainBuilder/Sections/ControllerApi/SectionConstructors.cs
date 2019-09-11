using System;

namespace Bedrock.DomainBuilder.Sections.ControllerApi
{
    public class SectionConstructors<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionConstructors(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), "#region Constructors");
            AddConstructors();
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddConstructors()
        {
            Add(false, Tab(2), string.Format("public {0}Controller", EntityName));
            Add(false, "(");
            Add(false, string.Format("I{0}ApplicationService {1}Service, ", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(false, "ISession session");
            Add(false, ")");
            Add(false, " : ");
            Add(string.Format("base(session, {0}Service) {{ }}", EntityName.LowerCaseFirstCharacter()));
        }
        #endregion
    }
}

using System;

namespace Bedrock.DomainBuilder.Sections.AppContext
{
    public class SectionUsingStatements<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionUsingStatements(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISession Methods
        public override string Build()
        {
            Add("using System;");
            Add(string.Format("using {0};", Settings.NamespaceEntity), NewLine());

            Add(string.Format("using {0}.Data.Repository.Interface;", Settings.NamespaceShared));
            Add(string.Format("using {0}.Session.Implementation;", Settings.NamespaceShared));
			Add(string.Format("using {0}.Session.Interface;", Settings.NamespaceShared));

			return base.Build();
        }
        #endregion
    }
}


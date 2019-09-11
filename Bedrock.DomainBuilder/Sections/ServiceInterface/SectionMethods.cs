using System;

namespace Bedrock.DomainBuilder.Sections.ServiceInterface
{
    public class SectionMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            BuildRootMethods();
            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildRootMethods()
        {
            Add(Tab(2), "#region Methods (Root)");

            if (Settings.IsIncludeCrud)
            {
                Add(Tab(2), string.Format("IEnumerable<{0}> Get{0}All();", EntityName), NewLine());
                Add(Tab(2), string.Format("{0} Get{0}ById(int id);", EntityName), NewLine());
                Add(Tab(2), string.Format("IServiceResponse<{0}, IBedrockEntity> Add{0}({0} {1});", EntityName, EntityName.LowerCaseFirstCharacter()), NewLine());

                if (Settings.IsIncludeCrudEntity)
                    Add(Tab(2), string.Format("IServiceResponse<{0}, IBedrockEntity> Add{0}({0} {1}, bool isAddChildren);", EntityName, EntityName.LowerCaseFirstCharacter()), NewLine());

                Add(Tab(2), string.Format("IServiceResponse<{0}, IBedrockEntity> Update{0}({0} {1});", EntityName, EntityName.LowerCaseFirstCharacter()), NewLine());

                if (Settings.IsIncludeCrudEntity)
                    Add(Tab(2), string.Format("IServiceResponse<{0}, IBedrockEntity> Update{0}({0} {1}, bool isUpdateChildren, bool isAddChildren, bool isDeleteChildren);", EntityName, EntityName.LowerCaseFirstCharacter()), NewLine());

                if (Settings.IsIncludeCrudEntity)
                    Add(Tab(2), "void Delete(int id);, NewLine()");
                else
                    Add(Tab(2), string.Format("void Delete({0} {1});", EntityName, EntityName.LowerCaseFirstCharacter()));
            }

            Add(false, Tab(2), "#endregion");
        }
        #endregion
    }
}

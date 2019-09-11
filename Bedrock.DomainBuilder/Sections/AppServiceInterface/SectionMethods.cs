using System;

namespace Bedrock.DomainBuilder.Sections.AppServiceInterface
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
            BuildMethods();
            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildMethods()
        {
            Add(Tab(2), "#region Methods");

            if (Settings.IsIncludeCrud)
            {
                Add(Tab(2), string.Format("Task<IEnumerable<{0}Contract>> Get{0}All();", EntityName), NewLine());
                Add(Tab(2), string.Format("Task<{0}Contract> Get{0}ById(int id);", EntityName), NewLine());
                Add(Tab(2), string.Format("Task<IServiceResponse<{0}, {0}Contract>> Add{0}({0}Contract {1}Contract);", EntityName, EntityName.LowerCaseFirstCharacter()), NewLine());
                Add(Tab(2), string.Format("Task<IServiceResponse<{0}, {0}Contract>> Update{0}({0}Contract {1}Contract);", EntityName, EntityName.LowerCaseFirstCharacter()), NewLine());
                Add(Tab(2), string.Format("Task Delete{0}(int id);", EntityName));
            }

            Add(false, Tab(2), "#endregion");
        }
        #endregion
    }
}

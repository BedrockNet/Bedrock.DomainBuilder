using System;

namespace Bedrock.DomainBuilder.Sections.AppService
{
    public class SectionInterfaceMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionInterfaceMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(NewLine(), Tab(2), string.Format("#region I{0}ApplicationService Methods", EntityName));
            AddInterfaceMethods();
            Add(false, Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddInterfaceMethods()
        {
            if (Settings.IsIncludeCrud)
            {
                AddGetAll();
                AddGetById();
                AddAdd();
                AddUpdate();
                AddDelete();
            }
        }

        private void AddGetAll()
        {
            Add(Tab(2), string.Format("public Task<IEnumerable<{0}Contract>> Get{0}All()", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), "return Task.Run(() =>");
            Add(Tab(3), "{");
            Add(Tab(4), string.Format("var {0} = {1}Service.Get{1}All();", EntityNamePluralized.LowerCaseFirstCharacter(), EntityName));
            Add(Tab(4), string.Format("return {0}Contract.Create().MapToModelsFromDomainModels({1});", EntityName, EntityNamePluralized.LowerCaseFirstCharacter()));
            Add(Tab(3), "});");
            Add(Tab(2), "}", NewLine());
        }

        private void AddGetById()
        {
            Add(Tab(2), string.Format("public Task<{0}Contract> Get{0}ById(int id)", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), "return Task.Run(() =>");
            Add(Tab(3), "{");
            Add(Tab(4), string.Format("var {0} = {1}Service.Get{1}ById(id);", EntityName.LowerCaseFirstCharacter(), EntityName, EntityName));
            Add(Tab(4), string.Format("return {0}Contract.Create().MapToModelFromDomainModel({1});", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(3), "});");
            Add(Tab(2), "}", NewLine());
        }

        private void AddAdd()
        {
            Add(Tab(2), string.Format("public Task<IServiceResponse<{0}, {0}Contract>> Add{0}({0}Contract {1}Contract)", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("var {0}ToAdd = {0}Contract.MapToDomainModelFromModel({0}Contract);", EntityName.LowerCaseFirstCharacter()), NewLine());
            Add(Tab(3), "return Task.Run(() =>");
            Add(Tab(3), "{");

            if (!Settings.IsIncludeCrudEntity)
                Add(Tab(4), string.Format("{0}ToAdd.SetStateAdded();", EntityName.LowerCaseFirstCharacter()));

            Add(Tab(4), string.Format("var response = {0}Service.Add{0}({1}ToAdd);", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(4), string.Format("return Response(response.Entity, {0}Contract, response.ValidationState);", EntityName.LowerCaseFirstCharacter()));
            Add(Tab(3), "});");
            Add(Tab(2), "}", NewLine());
        }

        private void AddUpdate()
        {
            Add(Tab(2), string.Format("public Task<IServiceResponse<{0}, {0}Contract>> Update{0}({0}Contract {1}Contract)", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("var {0}ToUpdate = {0}Contract.MapToDomainModelFromModel({0}Contract);", EntityName.LowerCaseFirstCharacter()), NewLine());
            Add(Tab(3), "return Task.Run(() =>");
            Add(Tab(3), "{");
            Add(Tab(4), string.Format("var response = {0}Service.Update{0}({1}ToUpdate);", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(4), string.Format("return Response(response.Entity, {0}Contract, response.ValidationState);", EntityName.LowerCaseFirstCharacter()));
            Add(Tab(3), "});");
            Add(Tab(2), "}", NewLine());
        }

        private void AddDelete()
        {
            Add(Tab(2), string.Format("public Task Delete{0}(int id)", EntityName));
            Add(Tab(2), "{");

            if (Settings.IsIncludeCrudEntity)
                Add(Tab(3), string.Format("return Task.Run(() => {0}Service.Delete(id));", EntityName));
            else
            {
                Add(Tab(3), "return Task.Run(() =>");
                Add(Tab(3), "{");
                Add(Tab(4), string.Format("var {0}ToDelete = {1}Service.Get{1}ById(id);", EntityName.LowerCaseFirstCharacter(), EntityName));
                Add(Tab(4), string.Format("{0}ToDelete.SetStateDeleted();", EntityName.LowerCaseFirstCharacter()));
                Add(Tab(4), string.Format("{0}Service.Delete({1}ToDelete);", EntityName, EntityName.LowerCaseFirstCharacter()));
                Add(Tab(3), "});");
            }

            Add(Tab(2), "}");
        }
        #endregion
    }
}

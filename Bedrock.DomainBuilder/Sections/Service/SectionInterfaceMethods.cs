using System;
using System.Collections.Generic;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.Service
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
            AddRootMethods();
            return base.Build();
        }
        #endregion

        #region Private Methods
        private void AddRootMethods()
        {
            Add(Tab(2), string.Format("#region I{0}Service Methods (Root)", EntityName));

            if (Settings.IsIncludeCrud)
            {
                AddGetAll();
                AddGetById();
                AddAddMethod();

                if (Settings.IsIncludeCrudEntity)
                    AddAddMethodWithOverload();

                AddUpdateMethod();

                if (Settings.IsIncludeCrudEntity)
                    AddUpdateMethodWithOverload();

                AddDelete();
            }

            Add(false, Tab(2), "#endregion");
        }

        private void AddGetAll()
        {
            Add(Tab(2), string.Format("public IEnumerable<{0}> Get{0}All()", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("return Context.{0}.ToArray();", EntityNamePluralized));
            Add(Tab(2), "}", NewLine());
        }

        private void AddGetById()
        {
            var properties = GetPropertiesForComparison(EntityName);

            if (properties.Count() != 1)
            {
                Pass.Builder.BuilderWarning(Pass.BuildSection.ToString(), GetWarningMessage());
                return;
            }

            Add(Tab(2), string.Format("public {0} Get{0}ById(int id)", EntityName));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("return Context.{0}.Find(id);", EntityNamePluralized));
            Add(Tab(2), "}", NewLine());
        }

        private void AddAddMethod()
        {
            Add(Tab(2), string.Format("public IServiceResponse<{0}, IBedrockEntity> Add{0}({0} {1})", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "{");

            if (Settings.IsIncludeCrudEntity)
                Add(Tab(3), string.Format("return Add{0}({1}, {2});", EntityName, EntityName.LowerCaseFirstCharacter(), Settings.IsRecursiveCrud.ToString().ToLower()));
            else
            {
                Add(Tab(3), string.Format("if({0} == null)", EntityName.LowerCaseFirstCharacter()));
                Add(Tab(4), string.Format("throw new ArgumentNullException(\"{0}\");", EntityName.LowerCaseFirstCharacter()), NewLine());
                Add(Tab(3), string.Format("Context.{0}.ApplyChanges({1});", EntityNamePluralized, EntityName.LowerCaseFirstCharacter()), NewLine());
                Add(Tab(3), string.Format("return Response<{0}, IBedrockEntity>({1});", EntityName, EntityName.LowerCaseFirstCharacter()));
            }

            Add(Tab(2), "}", NewLine());
        }

        private void AddAddMethodWithOverload()
        {
            Add(Tab(2), string.Format("public IServiceResponse<{0}, IBedrockEntity> Add{0}({0} {1}, bool isAddChildren)", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("if({0} == null)", EntityName.LowerCaseFirstCharacter()));
            Add(Tab(4), string.Format("throw new ArgumentNullException(\"{0}\");", EntityName.LowerCaseFirstCharacter()), NewLine());
            Add(Tab(3), string.Format("var {0}ToAdd = {1}.Create({0}, isAddChildren);", EntityName.LowerCaseFirstCharacter(), EntityName));
            Add(Tab(3), string.Format("Context.{0}.Add({1}ToAdd);", EntityNamePluralized, EntityName.LowerCaseFirstCharacter()), NewLine());
            Add(Tab(3), string.Format("return Response<{0}, IBedrockEntity>({1}ToAdd);", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "}", NewLine());
        }

        private void AddUpdateMethod()
        {
            IEnumerable<string> properties = null;

            if (!Settings.IsIncludeCrudEntity)
            {
                properties = GetPropertiesForComparison(EntityName);

                if (properties.Count() == 0)
                {
                    Pass.Builder.BuilderWarning(Pass.BuildSection.ToString(), GetWarningMessage());
                    return;
                }
            }

            Add(Tab(2), string.Format("public IServiceResponse<{0}, IBedrockEntity> Update{0}({0} {1})", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "{");

            if (Settings.IsIncludeCrudEntity)
                Add(Tab(3), string.Format("return Update{0}({1}, {2}, {2}, {2});", EntityName, EntityName.LowerCaseFirstCharacter(), Settings.IsRecursiveCrud.ToString().ToLower()));
            else
            {
                var equalityString = GetEqualityString(properties);
                var propertiesList = string.Join(", ", properties);

                Add(Tab(3), string.Format("if({0} == null)", EntityName.LowerCaseFirstCharacter()));
                Add(Tab(4), string.Format("throw new ArgumentNullException(\"{0}\");", EntityName.LowerCaseFirstCharacter()), NewLine());
                Add(Tab(3), string.Format("var is{0}Exists = Context.{1}.Any(e => {2});", EntityName, EntityNamePluralized, equalityString), NewLine());
                Add(Tab(3), string.Format("if (!is{0}Exists)", EntityName));
                Add(Tab(4), string.Format("throw new KeyNotFoundException(\"No {0} found for key(s) {1}\");", EntityName, propertiesList), NewLine());
                Add(Tab(3), string.Format("Context.{0}.ApplyChanges({1});", EntityNamePluralized, EntityName.LowerCaseFirstCharacter()), NewLine());
                Add(Tab(3), string.Format("return Response<{0}, IBedrockEntity>({1});", EntityName, EntityName.LowerCaseFirstCharacter()));
            }

            Add(Tab(2), "}", NewLine());
        }

        private void AddUpdateMethodWithOverload()
        {
            var properties = GetPropertiesForComparison(EntityName);

            if (properties.Count() == 0)
            {
                Pass.Builder.BuilderWarning(Pass.BuildSection.ToString(), GetWarningMessage());
                return;
            }

            var equalityString = GetEqualityString(properties);
            var propertiesList = string.Join(", ", properties);

            Add(Tab(2), string.Format("public IServiceResponse<{0}, IBedrockEntity> Update{0}({0} {1}, bool isUpdateChildren, bool isAddChildren, bool isDeleteChildren)", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(2), "{");
            Add(Tab(3), string.Format("if({0} == null)", EntityName.LowerCaseFirstCharacter()));
            Add(Tab(4), string.Format("throw new ArgumentNullException(\"{0}\");", EntityName.LowerCaseFirstCharacter()), NewLine());
            Add(Tab(3), string.Format("var existing{0} = Context.{1}.FirstOrDefault(e => {2});", EntityName, EntityNamePluralized, equalityString), NewLine());
            Add(Tab(3), string.Format("if(existing{0} == null)", EntityName));
            Add(Tab(4), string.Format("throw new KeyNotFoundException(\"No {0} found for key(s) {1}\");", EntityName, propertiesList), NewLine());
            Add(Tab(3), string.Format("existing{0}.Update({1}, isUpdateChildren, isAddChildren, isDeleteChildren);", EntityName, EntityName.LowerCaseFirstCharacter()));
            Add(Tab(3), string.Format("Context.{0}.Update(existing{1});", EntityNamePluralized, EntityName), NewLine());
            Add(Tab(3), string.Format("return Response<{0}, IBedrockEntity>(existing{0});", EntityName));
            Add(Tab(2), "}", NewLine());
        }

        private void AddDelete()
        {
            var properties = GetPropertiesForComparison(EntityName);

            if (properties.Count() != 1)
            {
                Pass.Builder.BuilderWarning(Pass.BuildSection.ToString(), GetWarningMessage());
                return;
            }

            if (Settings.IsIncludeCrudEntity)
                Add(Tab(2), "public void Delete(int id)");
            else
                Add(Tab(2), string.Format("public void Delete({0} {1})", EntityName, EntityName.LowerCaseFirstCharacter()));

            Add(Tab(2), "{");

            if (Settings.IsIncludeCrudEntity)
            {
                Add(Tab(3), string.Format("var {0}ToDelete = Update{1}(new {1} {{ {2} = id }}, false, true, true).Entity;", EntityName.LowerCaseFirstCharacter(), EntityName, properties.First()));
                Add(Tab(3), string.Format("Context.{0}.Remove({1}ToDelete);", EntityNamePluralized, EntityName.LowerCaseFirstCharacter()));
            }
            else
                Add(Tab(3), string.Format("Context.{0}.ApplyChanges({1});", EntityNamePluralized, EntityName.LowerCaseFirstCharacter()));

            Add(Tab(2), "}");
        }

        private string GetEqualityString(IEnumerable<string> properties)
        {
            if (properties.Count() == 1)
                return string.Format("e.{0} == {1}.{0}", properties.First(), EntityName.LowerCaseFirstCharacter());
            else
                return string.Join(" && ", properties.Select(p => "e." + p + " == " + EntityName.LowerCaseFirstCharacter() + "." + p));
        }

        private string GetWarningMessage()
        {
            return string.Format("Unable to create \"AddById()\" method  for service \"{0}Service\".  Unable to find key or Identity column for {1}.", EntityName);
        }
        #endregion
    }
}

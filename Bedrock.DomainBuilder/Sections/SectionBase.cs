using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

using Bedrock.DomainBuilder.EntityFramework;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Sections
{
    public abstract class SectionBase<T> : ISection
            where T : struct, IConvertible
    {
        #region Fields
        private object _lockSemaphore = new object();
        #endregion

        #region Constructors
        public SectionBase(BuildPass<T> pass) : this(pass, false) { }

        public SectionBase(BuildPass<T> pass, bool isInnerBuilder)
        {
            Pass = pass;
            Container = new StringBuilder();
            IsInnerBuilder = isInnerBuilder;
        }
        #endregion

        #region Protected Properties
        protected BuildPass<T> Pass { get; set; }

        protected BuildSettings Settings
        {
            get
            {
                lock (LockSemaphore)
                {
                    return Pass.Settings;
                }
            }
        }

        protected StringBuilder Container { get; set; }

        protected bool IsInnerBuilder { get; set; }

        protected virtual IEnumerable<EntityType> EntityTypes
        {
            get
            {
                return Pass
                      .EntityHelper
                      .EntityTypes
                      .Where(i => !Settings.EntityExclusions.Any(e => i.Name.StartsWith(e, StringComparison.CurrentCultureIgnoreCase))
                                      && (!Settings.IsUsePropertyInclusions || Settings.PropertyInclusions.Any(e => i.Name.Equals(e, StringComparison.CurrentCultureIgnoreCase))))
                      .ToArray();
            }
        }

        protected string EntityName
        {
            get
            {
                var returnValue = string.Empty;

                if (Pass.EntityType != null)
                    returnValue = Pass
                                    .Code
                                    .Escape(Pass.EntityType);

                return returnValue;
            }
        }

        protected string EntityNameSingularized
        {
            get
            {
                return Pass
                        .PluralizationService
                        .Singularize(EntityName);
            }
        }

        protected string EntityNamePluralized
        {
            get
            {
                return Pass
                        .PluralizationService
                        .Pluralize(EntityName);
            }
        }

        protected virtual IEnumerable<NavigationProperty> Navigations
        {
            get
            {
                return Pass
                        .EntityType
                        .NavigationProperties
                        .Where(np => np.DeclaringType == Pass.EntityType
                                        && np.ToEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many
                                        //&& !Pass.Enumerations.Contains(np.Name)
                                        && EntityTypes.Contains(Pass.EntityType)
                                        && EntityTypes.Contains(np.ToEndMember.GetEntityType()))
                        .ToArray();
            }
        }

        protected virtual IEnumerable<NavigationProperty> CollectionNavigations
        {
            get
            {
                return Pass
                        .EntityType
                        .NavigationProperties
                        .Where(np => np.DeclaringType == Pass.EntityType
                                        && np.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many
                                        && EntityTypes.Contains(Pass.EntityType)
                                        && EntityTypes.Contains(np.ToEndMember.GetEntityType()))
                        .ToArray();
            }
        }

        protected bool IsUseCommonPrimaryKey
        {
            get
            {
                return Settings.IsUseCommonKey
                                && Pass.EntityType.KeyMembers.Count == 1;
            }
        }

        protected bool IsRoot
        {
            get { return Pass.Roots.Contains(EntityName); }
        }

        protected bool IsEnumeration
        {
            get { return Pass.Enumerations.Contains(EntityName); }
        }

        protected object LockSemaphore
        {
            get { return _lockSemaphore; }
        }

        protected string SchemaCamelCased
        {
            get { return Settings.StoreSchemaNamespace.ToLower() == "dbo" ? string.Empty : Settings.StoreSchemaNamespace.ToCamelCase(); }
        }
        #endregion

        #region IBuilder Methods
        public virtual string Build()
        {
            if (Settings.IsNetCore)
                BuildCore();
            else
                BuildFramework();

            if (!IsInnerBuilder)
                Add();

            return Container.ToString();
        }
        #endregion

        #region Protected Methods
        protected virtual void BuildCore() { }

        protected virtual void BuildFramework() { }
        #endregion

        #region Protected Methods (Text)
        protected void Add(params object[] args)
        {
            Add(true, args);
        }

        protected void Add(bool isAddLine, params object[] args)
        {
            var add = AddReturn(isAddLine, args);
            Container.Append(add);
        }

        protected string AddReturn(params object[] args)
        {
            return AddReturn(true, args);
        }

        protected string AddReturn(bool isAddline, params object[] args)
        {
            var returnValue = string.Concat(args);

            if (isAddline)
                returnValue += NewLine();

            return returnValue;
        }

        protected string NewLine(int count = 1)
        {
            return string.Join
            (
                string.Empty, Enumerable.Range(1, count).Select(x => Environment.NewLine)
            );
        }

        protected string Tab(int count = 1)
        {
            return string.Join
            (
                string.Empty, Enumerable.Range(1, count).Select(x => "\t")
            );
        }
        #endregion

        #region Protected Methods (Entity)
        protected IEnumerable<string> GetPropertyNamesWithCommonKey()
        {
            var returnValue = GetPropertiesConfigIdAudit()
                                .Select(p => Pass.Code.Escape(p))
                                .ToList();

            string keyName = GetCommonKeyPropertyName();

            if (keyName != null && returnValue.Contains(keyName))
            {
                returnValue.Remove(keyName);
                returnValue.Insert(0, Settings.CommonKey);
            }

            return returnValue;
        }

        protected IEnumerable<EdmProperty> GetPropertiesConfigIdAudit()
        {
            var returnValue = GetPropertiesConfigAuditDeletable();

            if (!Settings.IsIncludePrimaryKey)
            {
                var identityProperty = GetIdentityProperty(EntityName);

                if (Pass.EntityType.KeyMembers.Count == 1)
                {
                    var primaryKey = Pass.EntityType.KeyMembers.Single().Name;
                    returnValue = returnValue.Where(p => p.Name != primaryKey);
                }
                else if (identityProperty != null)
                    returnValue = returnValue.Where(p => p.Name != identityProperty);
            }

            return returnValue;
        }

        protected IEnumerable<EdmProperty> GetPropertiesConfigAuditDeletable()
        {
            var returnValue = Pass.EntityType.Properties.AsEnumerable();

            if (!Settings.IsIncludeAuditFields)
            {
                var auditFields = Enum.GetValues(typeof(eAuditField))
                                        .Cast<eAuditField>()
                                        .Select(f => f.ToString().ToLower())
                                        .ToList();

                returnValue = returnValue.Where(p => !auditFields.Contains(p.Name.ToLower()));
            }

            if(!Settings.IsIncludeDeletableFields)
            {
                var deletableFields = Enum.GetValues(typeof(eDeletableField))
                                            .Cast<eDeletableField>()
                                            .Select(f => f.ToString().ToLower())
                                            .ToList();

                returnValue = returnValue.Where(p => !deletableFields.Contains(p.Name.ToLower()));
            }

            return returnValue;
        }

        protected Tuple<string, object> GetComparisonPropertyForUpdate(string entityName)
        {
            string property;
            object defaultValue;

            var entityType = Pass
                            .EntityHelper
                            .EntityTypes
                            .First(e => e.Name.ToLower() == Singularize(entityName.ToLower()));

            var keys = entityType.KeyMembers;

            if (keys.Count == 1)
            {
                var key = keys.Single();
                var type = Pass.Code.Tools.UnderlyingClrType(key.TypeUsage.EdmType);

                defaultValue = type.GetDefaultValue();

                if (Settings.IsUseCommonKey)
                    property = Settings.CommonKey;
                else
                    property = Pass.Code.Escape(key);
            }
            else
            {
                property = GetIdentityPropertyCommonKey(entityName);
                defaultValue = typeof(int).GetDefaultValue();
            }

            return new Tuple<string, object>(property, defaultValue);
        }

        protected IEnumerable<string> GetPropertiesForComparisonNoCommonKey(string entityName)
        {
            var returnValue = Enumerable.Empty<string>();
            var isUseCommonKey = Settings.IsUseCommonKey;

            Lock(() =>
            {
                Settings.IsUseCommonKey = false;
                returnValue = GetPropertiesForComparison(entityName);
                Settings.IsUseCommonKey = isUseCommonKey;
            });

            return returnValue;
        }

        protected IEnumerable<string> GetPropertiesForComparison(string entityName)
        {
            var returnValue = new List<string>();

            var entityType = Pass
                            .EntityHelper
                            .EntityTypes
                            .First(e => e.Name.ToLower() == Singularize(entityName.ToLower()));

            var keys = entityType.KeyMembers;

            // If only one key, take it
            if (keys.Count() == 1)
            {
                var propertyName = Pass.Code.Escape(keys.First());

                if (Settings.IsUseCommonKey)
                    propertyName = Settings.CommonKey;

                returnValue.Add(propertyName);
            }
            // If more than one key, find any property with Identity flag, key or not
            else if (keys.Count() > 1)
            {
                var identityProperty = GetIdentityPropertyCommonKey(entityName);

                if (identityProperty != null)
                    returnValue.Add(identityProperty);
            }

            // If not above, take whatever you have (0 or many)
            // No key treated as all fields being a composite key
            if (!returnValue.Any())
            {
                returnValue.AddRange(keys.Select(k => Pass.Code.Escape(k)));
            }

            return returnValue;
        }

        protected string GetIdentityPropertyCommonKey(string entityName)
        {
            var returnValue = GetIdentityProperty(entityName);
            var keyName = GetCommonKeyPropertyName();

            if (returnValue == keyName)
                returnValue = Settings.CommonKey;

            return returnValue;
        }

        protected string GetIdentityProperty(string entityName)
        {
            string returnValue = null;

            var entityType = Pass
                            .EntityHelper
                            .EntityTypes
							.First(e => e.Name.ToLower() == entityName.ToLower());

            entityType.Properties.Each((p) =>
            {
                var identityProperty = p.MetadataProperties
                                        .FirstOrDefault(m => m.Name
                                                                .EndsWith(EntityHelper.STORE_GENERATED_PATTERN)
                                                                    && m.Value.ToString() == EntityHelper.IDENTITY);

                if (identityProperty != null)
                {
                    returnValue = Pass.Code.Escape(p);
                    return false;
                }

                return true;
            });

            return returnValue;
        }

        protected string GetCommonKeyPropertyName()
        {
            var returnValue = string.Empty;

            if (Settings.IsUseCommonKey)
            {
                var identityProperty = GetIdentityProperty(EntityName);

                if (Pass.EntityType.KeyMembers.Count() == 1)
                {
                    returnValue = Pass.EntityType.KeyMembers.Single().Name;
                }
                else if (identityProperty != null)
                {
                    returnValue = identityProperty;
                }
            }

            return returnValue;
        }

        protected Type GetPropertyType(string key)
        {
            Type returnValue = null;

            var keyProperty = Pass.EntityType.Properties.FirstOrDefault(p => Pass.Code.Escape(p) == key);

            if (keyProperty != null)
                returnValue = Pass.Code.Tools.UnderlyingClrType(keyProperty.TypeUsage.EdmType);

            return returnValue;
        }

        protected string GetPrimaryKeyType(EntityType entityType)
        {
            var keyType = Settings.PrimaryKeyType.ToString();

            if (entityType.KeyMembers.Count() == 1)
            {
                var property = entityType.KeyMembers.Single();
                keyType = Pass.Code.Escape(property.TypeUsage);
            }

            return keyType;
        }

        protected string Singularize(string value)
        {
            return Pass
					.PluralizationService
					.Singularize(value);
        }

        protected string Pluralize(string value)
        {
            return Pass
                    .PluralizationService
                    .Pluralize(value);
        }

        protected void Lock(Action action)
        {
            lock (LockSemaphore)
            {
                if (action != null)
                    action();
            }
        }
        #endregion
    }
}

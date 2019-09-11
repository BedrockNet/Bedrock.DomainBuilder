using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.Mapping
{
    public class SectionPublicMethods<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionPublicMethods(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region Protected Methods
        protected override void BuildCore()
        {
            Add(Tab(2), "#region IEntityTypeConfiguration Members");
            BuildMethodMapping();
            Add(false, Tab(2), "#endregion");
        }

        protected override void BuildFramework() { }
        #endregion

        #region Private Methods
        private void BuildMethodMapping()
        {
            Add(Tab(2), string.Format("public void Configure(EntityTypeBuilder<{0}> builder)", EntityNameSingularized));
            Add(Tab(2), "{");

            CreatePrimaryKeys();
            CreateProperties();
            CreateTableAndColumnMappings();
            CreateRelationships();

            Add(false, Tab(2), "}", NewLine());
        }

        private void CreatePrimaryKeys()
        {
            Add(Tab(3), "// Primary Key");

            if (Pass.EntityType.KeyMembers.Count() == 1)
            {
                var keyName = Settings.IsUseCommonKey ? Settings.CommonKey : Pass.EntityType.KeyMembers.Single().Name;

                Add(Tab(3), string.Format("builder.HasKey(t => t.{0});", keyName));
            }
            else
            {
                var identityProperty = GetIdentityProperty(EntityName);

                if (identityProperty != null && !Settings.IsIncludePrimaryKey)
                {
                    var keyName = Settings.IsUseCommonKey ? Settings.CommonKey : identityProperty;

                    Add(Tab(3), string.Format("builder.HasKey(t => t.{0});", keyName));
                }
                else
                {
                    var keys = Pass.EntityType.KeyMembers.OrderByDescending(o => o.Name);
                    Add(Tab(3), string.Format("builder.HasKey(t => new {{ {0} }});", string.Join(", ", keys.Select(m => "t." + m.Name))));
                }
            }

            Add();
        }

        private void CreateProperties()
        {
            Add(Tab(3), "// Properties");

            var keyName = GetCommonKeyPropertyName();

            Pass.EntityType.Properties.Each(p =>
            {
                var propertyName = p.Name;

                if (propertyName == keyName)
                    propertyName = Settings.CommonKey;

                var propertyConfiguration = GetPropertyConfiguration(p);
                var newLineTab = string.Concat(NewLine(), Tab(5));

                if (propertyConfiguration.Any())
                {
                    Add(Tab(3), string.Format("builder.Property(t => t.{0}){2}{1};", propertyName, string.Join(newLineTab, propertyConfiguration), newLineTab), NewLine());
                }
            });
        }

        private void CreateTableAndColumnMappings()
        {
            var tableName = (string)Pass.TableSet.MetadataProperties["Table"].Value ?? Pass.TableSet.Name;
            var schemaName = (string)Pass.TableSet.MetadataProperties["Schema"].Value;

            Add(Tab(3), "// Table & Column Mappings");

            if (schemaName == "dbo" || string.IsNullOrWhiteSpace(schemaName))
            {
                Add(Tab(3), string.Format("builder.ToTable(\"{0}\");", tableName));
            }
            else
            {
                Add(Tab(3), string.Format("builder.ToTable(\"{0}\", \"{1}\");", tableName, schemaName));
            }

            var keyName = GetCommonKeyPropertyName();

            Pass.EntityType.Properties.Each(p =>
            {
                var propertyName = p.Name;

                if (propertyName == keyName)
                    propertyName = Settings.CommonKey;

                Add(Tab(3), string.Format("builder.Property(t => t.{0}).HasColumnName(\"{1}\");", propertyName, Pass.PropertyToColumnMappings[p].Name));
            });

            Add();
        }

        private void CreateRelationships()
        {
            Add(Tab(3), "// Many to Many Relationships");

            CreateManyManyRelationships();

            Add(NewLine(), Tab(3), "// Foreign Key Relationships");

            CreateForeignKeyRelationships();
        }

        private void CreateManyManyRelationships()
        {
            // Find m:m relationshipsto configure 
            var manyManyRelationships = Pass
                                        .EntityType.NavigationProperties
                                        .Where(np => np.DeclaringType == Pass.EntityType
                                                        && np.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many
                                                        && np.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many
                                                        && np.RelationshipType.RelationshipEndMembers.First() == np.FromEndMember // <- ensures we only configure from one end
                                                        && EntityTypes.Contains(Pass.EntityType)
                                                        && EntityTypes.Contains(np.ToEndMember.GetEntityType()));

            if (manyManyRelationships.Any())
            {
                manyManyRelationships.Each(r =>
                {
                    var otherNavProperty = r.ToEndMember.GetEntityType().NavigationProperties.Where(n => n.RelationshipType == r.RelationshipType && n != r).Single();
                    var association = (AssociationType)r.RelationshipType;
                    var mapping = Pass.EntityHelper.Mappings.ManyToManyMappings[association];
                    var item1 = mapping.Item1;
                    var mappingTableName = (string)mapping.Item1.MetadataProperties["Table"].Value ?? item1.Name;
                    var mappingSchemaName = (string)item1.MetadataProperties["Schema"].Value;

                    // Need to ensure that FKs are declared in the same order as the PK properties on each principal type
                    var leftType = (EntityType)r.DeclaringType;
                    var leftKeyMappings = mapping.Item2[r.FromEndMember];
                    var leftColumns = string.Join(", ", leftType.KeyMembers.Select(m => "\"" + leftKeyMappings[m] + "\""));
                    var rightType = (EntityType)otherNavProperty.DeclaringType;
                    var rightKeyMappings = mapping.Item2[otherNavProperty.FromEndMember];
                    var rightColumns = string.Join(", ", rightType.KeyMembers.Select(m => "\"" + rightKeyMappings[m] + "\""));

                    Add(Tab(3), string.Format("builder.HasMany(t => t.{0})", Pass.Code.Escape(r)));
                    Add(Tab(4), string.Format(".WithMany(t => t.{0})", Pass.Code.Escape(otherNavProperty)));
                    Add(Tab(4), ".Map(m =>");
                    Add(Tab(4), "{");

                    if (mappingSchemaName == "dbo" || string.IsNullOrWhiteSpace(mappingSchemaName))
                    {
                        Add(Tab(5), string.Format("m.ToTable(\"{0}\");", mappingTableName));
                    }
                    else
                    {
                        Add(Tab(5), string.Format("m.ToTable(\"{0}\", \"{1}\");", mappingTableName, mappingSchemaName));
                    }

                    Add(Tab(5), string.Format("m.MapLeftKey({0});", leftColumns));
                    Add(Tab(5), string.Format("m.MapRightKey({0});", rightColumns));

                    Add(Tab(4), "});");
                });
            }
        }

        private void CreateForeignKeyRelationships()
        {
            // Find FK relationships that this entity is the dependent of
            var fkRelationships = Pass
                                    .EntityType
                                    .NavigationProperties
                                    .Where(np => np.DeclaringType == Pass.EntityType
                                                    && ((AssociationType)np.RelationshipType).IsForeignKey
                                                    && ((AssociationType)np.RelationshipType).ReferentialConstraints.Single().ToRole == np.FromEndMember
                                                    && EntityTypes.Contains(Pass.EntityType)
                                                    && EntityTypes.Contains(np.ToEndMember.GetEntityType()));

            if (fkRelationships.Any())
            {
                fkRelationships.Each(r =>
                {
                    var otherNavProperty = r.ToEndMember.GetEntityType().NavigationProperties.Where(n => n.RelationshipType == r.RelationshipType && n != r).Single();
                    var association = (AssociationType)r.RelationshipType;

                    if (r.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
                    {
                        Add(Tab(3), string.Format("builder.HasOne(t => t.{0})", Pass.Code.Escape(r)));
                    }
                    else
                    {
                        Add(Tab(3), string.Format("builder.HasOne(t => t.{0})", Pass.Code.Escape(r)));
                    }

                    if (r.FromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                    {
                        Add(Tab(4), string.Format(".WithMany(t =>t.{0})", Pass.Code.Escape(otherNavProperty)));

                        if (association.ReferentialConstraints.Single().ToProperties.Count == 1)
                        {
                            Add(Tab(4), string.Format(".HasForeignKey(d => d.{0})", association.ReferentialConstraints.Single().ToProperties.Single().Name));
                        }
                        else
                        {
                            var toProperties = association.ReferentialConstraints.Single().ToProperties.OrderByDescending(o => o.Name);
                            Add(Tab(4), string.Format(".HasForeignKey(d => new {{ {0} }})", string.Join(", ", toProperties.Select(p => "d." + p.Name))));
                        }
                    }
                    else
                        Add(Tab(4), string.Format(".WithOne(t => t.{0})", Pass.Code.Escape(otherNavProperty)));

                    if (Settings.IsCascadeOnDelete)
                        Add(Tab(4), ".Metadata.DeleteBehavior = DeleteBehavior.Cascade;");
                    else
                        Add(Tab(4), ".Metadata.DeleteBehavior = DeleteBehavior.Restrict;");
                });
            }
        }

        private List<string> GetPropertyConfiguration(EdmProperty property)
        {
            var type = (PrimitiveType)property.TypeUsage.EdmType;
            var isKey = Pass.EntityType.KeyMembers.Contains(property);
            var storeProp = Pass.PropertyToColumnMappings[property];
            var sgpFacet = storeProp.TypeUsage.Facets.SingleOrDefault(f => f.Name == "StoreGeneratedPattern");
            var storeGeneratedPattern = sgpFacet == null ? StoreGeneratedPattern.None : (StoreGeneratedPattern)sgpFacet.Value;

            var configLines = new List<string>();

            if (type.ClrEquivalentType == typeof(int) || type.ClrEquivalentType == typeof(decimal) || type.ClrEquivalentType == typeof(short) || type.ClrEquivalentType == typeof(long) || type.ClrEquivalentType == typeof(byte))
            {
                if (isKey && storeGeneratedPattern != StoreGeneratedPattern.Identity)
                {
                    configLines.Add(".ValueGeneratedNever()");
                }
                else if ((!isKey || Pass.EntityType.KeyMembers.Count > 1) && storeGeneratedPattern == StoreGeneratedPattern.Identity)
                {
                    configLines.Add(".ValueGeneratedOnAdd()");
                }
            }

            if (type.ClrEquivalentType == typeof(string) || type.ClrEquivalentType == typeof(byte[]))
            {
                if (!property.Nullable)
                {
                    configLines.Add(".IsRequired()");
                }

                var unicodeFacet = (Facet)property.TypeUsage.Facets.SingleOrDefault(f => f.Name == "IsUnicode");

                if (unicodeFacet != null && !(bool)unicodeFacet.Value)
                {
                    configLines.Add(".IsUnicode(false)");
                }

                var fixedLengthFacet = (Facet)property.TypeUsage.Facets.SingleOrDefault(f => f.Name == "FixedLength");
                var maxLengthFacet = (Facet)property.TypeUsage.Facets.SingleOrDefault(f => f.Name == "MaxLength");

                if (fixedLengthFacet != null && (bool)fixedLengthFacet.Value)
                {
                    configLines.Add($".HasColumnType(\"char({ maxLengthFacet.Value})\")");
                }
                else if (maxLengthFacet != null && !maxLengthFacet.IsUnbounded)
                {
                    configLines.Add(string.Format(".HasMaxLength({0})", maxLengthFacet.Value));

                    if (storeGeneratedPattern == StoreGeneratedPattern.Computed && type.ClrEquivalentType == typeof(byte[]))
                    {
                        if ((int)maxLengthFacet.Value == 8)
                        {
                            configLines.Add(".IsRowVersion()");
                        }
                        else
                        {
                            configLines.Add(".HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)");
                        }
                    }
                }
            }

            return configLines;
        }
        #endregion
    }
}

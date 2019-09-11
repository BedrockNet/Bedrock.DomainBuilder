﻿using System;
using System.Linq;

namespace Bedrock.DomainBuilder.Sections.Contract
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
            BuildConstructor();
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion

        #region Private Methods
        private void BuildConstructor()
        {
            if (!CollectionNavigations.Any() || IsEnumeration)
                return;

            Add(Tab(2), string.Format("public {0}{1}()", EntityName, Settings.ContractName));
            Add(Tab(2), "{");

            CollectionNavigations.Each(n =>
            {
                var type = Singularize(Pass.Code.Escape(n.ToEndMember.GetEntityType()));
                Add(Tab(3), Pass.Code.Escape(n), " = new List<", type, Settings.ContractName, ">();");
            });

            Add(Tab(2), "}");
        }
        #endregion
    }
}

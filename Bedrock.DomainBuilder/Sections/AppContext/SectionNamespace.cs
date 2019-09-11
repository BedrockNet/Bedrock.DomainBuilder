﻿using System;

namespace Bedrock.DomainBuilder.Sections.AppContext
{
    public class SectionNamespace<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionNamespace(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add("namespace ", Settings.NamespaceDomain);
            Add(false, "{");

            return base.Build();
        }
        #endregion
    }
}

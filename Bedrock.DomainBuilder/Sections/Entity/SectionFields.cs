﻿using System;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionFields<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionFields(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(Tab(2), "#region Fields");
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion
    }
}

﻿using System;

namespace Bedrock.DomainBuilder.Sections.AppServiceInterface
{
    public class SectionInterfaceClose<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionInterfaceClose(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            Add(false, Tab(), "}");
            return base.Build();
        }
        #endregion
    }
}

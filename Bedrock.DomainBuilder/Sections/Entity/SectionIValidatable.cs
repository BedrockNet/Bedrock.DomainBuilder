using System;

namespace Bedrock.DomainBuilder.Sections.Entity
{
    public class SectionIValidatable<T> : SectionBase<T>, ISection
        where T : struct, IConvertible
    {
        #region Constructors
        public SectionIValidatable(BuildPass<T> pass) : base(pass) { }
        #endregion

        #region ISection Methods
        public override string Build()
        {
            if (!Settings.IsValidatable || IsEnumeration)
                return string.Empty;

            Add(Tab(2), "#region IValidateableObject Methods");
            Add(Tab(2), "public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)");
            Add(Tab(2), "{");
            //Add(Tab(3), "ClearValidationResults();");
            Add(Tab(3), "ValidateInternal(validationContext);");
            Add(Tab(3), "return ValidationResults;");
            Add(Tab(2), "}");
            Add(Tab(2), "#endregion");

            return base.Build();
        }
        #endregion
    }
}

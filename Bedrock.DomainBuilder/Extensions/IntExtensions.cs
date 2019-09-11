namespace Bedrock.DomainBuilder
{
    public static class IntExtensions
    {
        #region Public Methods
        public static string AddThousandthsPlaceCommas(this int value)
        {
            return string.Format("{0:n0}", value);
        }
        #endregion
    }
}

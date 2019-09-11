using System.Threading.Tasks;

namespace Bedrock.DomainBuilder
{
    public struct PauseToken
    {
        #region Fields
        private readonly PauseTokenSource _source;
        #endregion

        #region Constructors
        internal PauseToken(PauseTokenSource source)
        {
            _source = source;
        }
        #endregion

        #region Properties
        public bool IsPaused
        {
            get { return _source != null && _source.IsPaused; }
        }
        #endregion

        #region Public Methods
        public Task WaitWhilePausedAsync()
        {
            return IsPaused
                    ? _source.WaitWhilePausedAsync()
                    : PauseTokenSource._completedTask;
        }
        #endregion
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace Bedrock.DomainBuilder
{
    public class PauseTokenSource
    {
        #region Fields
        private TaskCompletionSource<bool> _paused;
        internal static readonly Task _completedTask = Task.FromResult(true);
        #endregion

        #region Properties
        public bool IsPaused
        {
            get { return _paused != null; }
            set
            {
                if (value)
                {
                    Interlocked.CompareExchange(ref _paused, new TaskCompletionSource<bool>(), null);
                }
                else
                {
                    while (true)
                    {
                        var tcs = _paused;
                        if (tcs == null) return;

                        if (Interlocked.CompareExchange(ref _paused, null, tcs) == tcs)
                        {
                            tcs.SetResult(true);
                            break;
                        }
                    }
                }
            }
        }

        public PauseToken Token
        {
            get { return new PauseToken(this); }
        }
        #endregion

        #region Methods
        internal Task WaitWhilePausedAsync()
        {
            var cur = _paused;

            return cur != null
                    ? cur.Task
                    : _completedTask;
        }

        internal void PauseToken(bool isPaused)
        {
            IsPaused = isPaused;
        }
        #endregion
    }
}

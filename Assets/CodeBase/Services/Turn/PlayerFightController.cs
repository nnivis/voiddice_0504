using System.Threading;
using System.Threading.Tasks;

namespace CodeBase.Services.Turn
{
    // Аналог LocalHumanPlayerController из sivin — ждёт через TaskCompletionSource
    // пока GameFightHandler не вызовет CompleteTurn() (все кубики использованы или таймер вышел)
    public class PlayerFightController : IFightController
    {
        private TaskCompletionSource<bool> _turnTcs;

        public bool IsWaitingForTurn => _turnTcs != null && !_turnTcs.Task.IsCompleted;

        public Task RequestTurnAsync(CancellationToken ct)
        {
            _turnTcs = new TaskCompletionSource<bool>(
                TaskCreationOptions.RunContinuationsAsynchronously);

            ct.Register(() => _turnTcs.TrySetCanceled(ct));

            return _turnTcs.Task;
        }

        public void CompleteTurn()
        {
            if (!IsWaitingForTurn) return;
            _turnTcs.TrySetResult(true);
        }
    }
}

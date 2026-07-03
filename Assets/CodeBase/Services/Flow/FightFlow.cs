using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services.Flow
{
    // Аналог GameFlow из sivin — async-петля которая гоняет PlayTurnAsync до конца боя
    public class FightFlow
    {
        private readonly FightTurnSystem _turnSystem;
        private CancellationTokenSource _cts;

        public FightFlow(FightTurnSystem turnSystem)
        {
            _turnSystem = turnSystem ?? throw new ArgumentNullException(nameof(turnSystem));
        }

        public void Start()
        {
            Stop();
            _cts = new CancellationTokenSource();
            _ = RunAsync(_cts.Token);
        }

        public void Stop()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        private async Task RunAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                    await _turnSystem.PlayTurnAsync(ct);
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.LogError($"[FightFlow] Unhandled exception: {e}");
            }
        }
    }
}

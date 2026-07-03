using System;

namespace CodeBase.Services.GamePlay
{
    public class GamePlayMediator
    {
        public Action<GameFightEndReason> OnGameOver;
        public Action OnTurnExpired;

        public void NotifyGameOver(GameFightEndReason gameOverType) =>
            OnGameOver?.Invoke(gameOverType);

        public void NotifyTurnExpired() =>
            OnTurnExpired?.Invoke();
    }
}

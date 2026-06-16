using System;

namespace CodeBase.Services.GamePlay
{
    public class GamePlayMediator
    {
        public Action<GameFightEndReason> OnGameOver;

        public void NotifyGameOver(GameFightEndReason gameOverType)
        {
            OnGameOver?.Invoke(gameOverType);
        }
    }
}

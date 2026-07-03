using UnityEngine;

namespace CodeBase.Services.StateMachine.States
{
    // Заглушка: все бои в уровне пройдены (персонаж на последней клетке)
    // TODO: добавить UI победы над уровнем, переход на следующий уровень или финальный экран
    public class LevelAllFightsCompleteState : StateMachineBehavior
    {
        protected override void OnEnter()
        {
            Debug.Log("[LevelAllFightsCompleteState] Все бои пройдены! Заглушка.");
        }
    }
}

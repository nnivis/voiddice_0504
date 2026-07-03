using System.Collections.Generic;
using CodeBase.Domain.Dice;
using CodeBase.Domain.Enemy;
using CodeBase.Services.GamePlay;

namespace CodeBase.Services.Turn.Ai
{
    public interface IEnemyDiceStrategy
    {
        // Выбирает кубик, применяет его эффект и возвращает его.
        // Возвращает null если кубиков нет и был применён fallback-урон.
        Dice SelectAndApply(List<Dice> availableDice, Enemy enemy, AbilityMediator mediator);
    }
}

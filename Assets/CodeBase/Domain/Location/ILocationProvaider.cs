using CodeBase.Domain.Dice;
using CodeBase.Domain.Enemy;
using CodeBase.Domain.RollDice;

namespace CodeBase.Domain.Location
{
    public interface ILocationProvaider
    {
        RollDiceConfig rollDiceConfig { get; }
        EnemyFactory enemyFactory { get; }
        DiceFactory diceFactory { get; }
    }
}

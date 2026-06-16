namespace CodeBase.Domain.Dice
{
    public interface IDiceConfigProvider
    {
        DiceConfig GetConfig(DiceType type);
        DiceType GetRandomType(DiceType currentType);
    }
}

namespace CodeBase.Domain.Player.State
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IPlayerState;
    }
}

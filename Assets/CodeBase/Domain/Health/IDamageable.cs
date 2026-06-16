namespace CodeBase.Domain.Health
{
    public interface IDamageable
    {
        void ApplyDamage(int damage);
        void ApplyHealing(int amount);
    }
}

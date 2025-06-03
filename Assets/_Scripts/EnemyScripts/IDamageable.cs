public interface IDamageable
{
    void TakeDamage(int value);
    int Health { get; set; }
}
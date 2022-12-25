using System.Collections;
public interface IDamageable {
    public void TakeDamage(float damage, DamageType type);
    public IEnumerator TakeDamageOverTime(float damage, DamageType type);
}

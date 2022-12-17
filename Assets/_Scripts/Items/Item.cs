

[System.Serializable]
public abstract class Item
{
    public string Name;
    public virtual void Update(Character player, int stacks){

    }
    public virtual void OnHit(Enemy enemy, int stacks){

    }
}
public class HealighItem : Item {
    public override void Update(Character player, int stacks){
        player.HpRecovery(5);
    }
    
}
public class FireDamageItem : Item{
    public override void OnHit(Enemy enemy, int stacks){
        enemy.TakeDamage(5 + (2 * stacks), DamageType.Fire);
    }
}



[System.Serializable]
public abstract class Item
{
    public string Name;
    public virtual void Update(Player player, int stacks){

    }
    public virtual void OnHit(Enemy enemy, int stacks){

    }
    public virtual void OnPickup(Player player, int stacks){

    }
    public virtual void OnDrop(Player player){
        
    }
}
public class HealighItem : Item {
    public override void Update(Player player, int stacks){
        player.HpRecovery(5 * stacks);
    }
    
}
public class FireDamageItem : Item{
    public override void OnHit(Enemy enemy, int stacks){
        enemy.TakeDamage(5 + (2 * stacks), DamageType.Fire);
    }
}
public class IncreaseMaxHp : Item{
    public override void OnPickup(Player player, int stacks){
        player.SetMaxHp(20);

    }
    public override void OnDrop(Player player){
        player.SetMaxHp(-20);
    }

}

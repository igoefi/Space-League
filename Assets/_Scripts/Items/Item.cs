

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

    // methods for weapon items
    // public virtual void OnPickup(Weapon weapon, int stacks){

    // }
    // public virtual void OnDrop(Weapon weapon){
        
    // }
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
        player.SetBonusMaxHp(20);
    }
    public override void OnDrop(Player player){
        player.SetBonusMaxHp(-20);
    }

}
public class Stamina : Item{
    public override void OnPickup(Player player, int stacks){
        player.SetBonusStaminaRegen(5);
    }
    public override void OnDrop(Player player){
        player.SetBonusStaminaRegen(-5);
    }
}
public class SpeedItem : Item{
    public override void OnPickup(Player player, int stacks){
        player.MovementRef.SetBonusWalkSpeed(3);
        player.MovementRef.SetBonusDashSpeed(3);
    }
    public override void OnDrop(Player player){
        player.MovementRef.SetBonusWalkSpeed(-3);
        player.MovementRef.SetBonusDashSpeed(-3); 
    }
}

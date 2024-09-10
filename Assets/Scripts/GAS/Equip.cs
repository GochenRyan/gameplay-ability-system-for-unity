using GAS.Runtime;

public class Equip : AbstractAbility<EquipAsset>
{
    public EquipmentActor EquipActor {  get; set; }

    public Equip(EquipAsset abilityAsset) : base(abilityAsset)
    {
        EquipActor = null;
    }

    public override AbilitySpec CreateSpec(AbilitySystemComponent owner)
    {
        return new EquipSpec(this, owner);
    }
}

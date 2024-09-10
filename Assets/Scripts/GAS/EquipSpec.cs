using GAS.Runtime;

public class EquipSpec : AbilitySpec<Equip>
{
    public EquipSpec(Equip ability, AbilitySystemComponent owner) : base(ability, owner)
    {
    }

    public override void ActivateAbility(params object[] args)
    {


        TryEndAbility();
    }

    public override void CancelAbility()
    {
    }

    public override void EndAbility()
    {
    }
}

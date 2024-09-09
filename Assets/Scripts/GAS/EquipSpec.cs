using GAS.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSpec : AbilitySpec<Equip>
{
    public EquipSpec(Equip ability, AbilitySystemComponent owner) : base(ability, owner)
    {
    }

    public override void ActivateAbility(params object[] args)
    {
        var equipModel = Data.EquipModel;
        
        //TODO: Add abilities and effects from equipment
        foreach(string effect in equipModel.Effects)
        {
            //Owner.ApplyGameplayEffectToSelf();
        }

        foreach(string effect in equipModel.Effects)
        {
            //Owner.GrantAbility()
        }

        TryEndAbility();
    }

    public override void CancelAbility()
    {
        var equipModel = Data.EquipModel;
        //TODO: Remove abilities and effects from equipment
        foreach (string effect in equipModel.Effects)
        {
            //Owner.ApplyGameplayEffectToSelf();
        }

        foreach (string effect in equipModel.Effects)
        {
            //Owner.GrantAbility()
        }
    }

    public override void EndAbility()
    {
    }
}

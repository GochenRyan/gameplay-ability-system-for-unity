using GAS.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpec : AbilitySpec<Attack>
{
    public AttackSpec(Attack ability, AbilitySystemComponent owner) : base(ability, owner)
    {
    }

    public override void ActivateAbility(params object[] args)
    {

        //Owner.ApplyGameplayEffectTo()

        TryEndAbility();
    }

    public override void CancelAbility()
    {
    }

    public override void EndAbility()
    {
    }
}

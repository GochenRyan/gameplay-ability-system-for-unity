using GAS.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AbstractAbility<AttackAsset>
{
    public Attack(AttackAsset abilityAsset) : base(abilityAsset)
    {

    }

    public override AbilitySpec CreateSpec(AbilitySystemComponent owner)
    {
        return new AttackSpec(this, owner);
    }
}

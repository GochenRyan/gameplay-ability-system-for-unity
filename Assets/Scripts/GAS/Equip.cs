using GAS.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : AbstractAbility<EquipAsset>
{
    public EquipmentModel EquipModel {  get; set; }

    public Equip(EquipAsset abilityAsset) : base(abilityAsset)
    {
        EquipModel = null;
    }

    public override AbilitySpec CreateSpec(AbilitySystemComponent owner)
    {
        return new EquipSpec(this, owner);
    }
}

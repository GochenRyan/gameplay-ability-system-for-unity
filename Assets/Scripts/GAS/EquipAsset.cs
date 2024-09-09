using GAS.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAsset : AbilityAsset
{
    public override Type AbilityType() => typeof(Equip);
}

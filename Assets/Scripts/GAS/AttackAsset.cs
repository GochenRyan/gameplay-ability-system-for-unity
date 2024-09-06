using GAS.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAsset : AbilityAsset
{
    public GameObject bulletPrefab;
    public override Type AbilityType() => typeof(Attack);
}

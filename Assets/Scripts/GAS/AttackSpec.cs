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
        var bullet = Object.Instantiate(Data.bulletPrefab).GetComponent<Bullet>();
        var transform = Owner.transform;
        bullet.Init(transform.position, transform.up, 10, Owner.AttrSet<AS_Fight>().Attack.CurrentValue);
        TryEndAbility();
    }

    public override void CancelAbility()
    {
    }

    public override void EndAbility()
    {
    }
}

using GAS.Runtime;
using Model;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class EnemyActor : Actor
{
    [NonSerialized, OdinSerialize]
    public EnemyModel EnemyModel;

    public EnemyActor(Actor parent) : base(parent) { }

    public void CreateByTID(int tID)
    {
        if (!DataManager.Instance.EnemyMap.ContainsKey(tID))
        {
            throw new Exception($"Enemy map does not have key: {tID}");
        }

        var enemyDefine = DataManager.Instance.EnemyMap[tID];

        List<string> dynamicEffects = new List<string>(enemyDefine.Effects);
        List<string> dynamicAbilities = new List<string>(enemyDefine.Abilities);
        var position = new Vector2(
            UnityEngine.Random.Range(-8f, 8f),
            UnityEngine.Random.Range(-4f, 4f));

        EnemyModel = new EnemyModel
        {
            ID = GenerateID(),
            TID = enemyDefine.TID,
            MaxHP = enemyDefine.HP,
            HP = enemyDefine.HP,
            Attack = enemyDefine.Attack,
            Defense = enemyDefine.Defense,
            Speed = enemyDefine.Speed,
            Abilities = dynamicAbilities,
            Effects = dynamicEffects,
            Position = position
        };


        Create();
    }

    public List<GameplayEffect> MakePlayerModelGE()
    {
        var ges = new List<GameplayEffect>();
        foreach (var geName in EnemyModel.Effects)
        {
            var op = Addressables.LoadAssetAsync<GameplayEffectAsset>(geName);
            var asset = op.WaitForCompletion();
            var ge = new GameplayEffect(asset);
            ges.Add(ge);
        }
        return ges;
    }

    public List<AbstractAbility> MakeAbilities()
    {
        var abilities = new List<AbstractAbility>();
        foreach (var abilityName in EnemyModel.Abilities)
        {
            var op = Addressables.LoadAssetAsync<AbilityAsset>(abilityName);
            var asset = op.WaitForCompletion();
            var ability = Activator.CreateInstance(asset.AbilityType(), args: asset) as AbstractAbility;
            abilities.Add(ability);
        }
        return abilities;
    }
}

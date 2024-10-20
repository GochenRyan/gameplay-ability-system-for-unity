using GAS.Runtime;
using Model;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

[Serializable]
public class PlayerActor : Actor
{
    [NonSerialized, OdinSerialize]
    public PlayerModel PlayerModel;

    public PlayerActor(Actor parent) : base(parent) { }

    public void CreateByTID(int tID)
    {
        if (!DataManager.Instance.PlayerMap.ContainsKey(tID))
        {
            throw new Exception($"Player map does not have key: {tID}");
        }

        var playerDefine = DataManager.Instance.PlayerMap[tID];

        List<string> dynamicEffects = new List<string>(playerDefine.Effects);
        List<string> dynamicAbilities = new List<string>(playerDefine.Abilities);

        PlayerModel = new PlayerModel
        {
            ID = GenerateID(),
            TID = playerDefine.TID,
            MaxHP = playerDefine.HP,
            HP = playerDefine.HP,
            Attack = playerDefine.Attack,
            Defense = playerDefine.Defense,
            Speed = playerDefine.Speed,
            Abilities = dynamicAbilities,
            Effects = dynamicEffects
        };
        Create();
    }

    public List<GameplayEffect> MakePlayerModelGE()
    {
        var ges = new List<GameplayEffect>();
        foreach (var geName in PlayerModel.Effects)
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
        foreach (var abilityName in PlayerModel.Abilities)
        {
            var op = Addressables.LoadAssetAsync<AbilityAsset>(abilityName);
            var asset = op.WaitForCompletion();
            var ability = Activator.CreateInstance(asset.AbilityType(), args: asset) as AbstractAbility;
            abilities.Add(ability);
        }
        return abilities;
    }
}

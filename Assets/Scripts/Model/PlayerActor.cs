using GAS.Runtime;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;

[Serializable]
public class PlayerActor
{
    [NonSerialized, OdinSerialize]
    public PlayerModel PlayerModel;

    public static event Action<PlayerActor> Created;
    public static event Action<PlayerActor> Destroy;

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
        AllPlayerActor.Add(PlayerModel.ID, this);
        Created?.Invoke(this);
    }

    public void DestoryActor()
    {
        AllPlayerActor.Remove(PlayerModel.ID);
        Destroy?.Invoke(this);
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

    public static int GenerateID()
    {
        var IDs = AllPlayerActor.Keys;
        if (IDs.Count == 0)
        {
            return 1;
        }
        else
        {
            return IDs.Max() + 1;
        }
    }

    [NonSerialized, OdinSerialize]
    public static Dictionary<int, PlayerActor> AllPlayerActor = new Dictionary<int, PlayerActor>();
}

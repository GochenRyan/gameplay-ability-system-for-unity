using GAS.Runtime;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class PlayerActor
{
    [NonSerialized, OdinSerialize]
    public int TID;
    [NonSerialized, OdinSerialize]
    public PlayerModel PlayerModel;

    public static event Action<PlayerActor> Created;
    public static event Action<PlayerActor> Destroy;

    public void CreateByTID(int tID)
    {
        if (DataManager.Instance.PlayerMap.ContainsKey(TID))
        {
            throw new Exception($"Player map does not have key: {tID}");
        }

        var playerDefine = DataManager.Instance.PlayerMap[TID];

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

    //public List<GameplayEffect> MakePlayerModelGE()
    //{
    //    GASHelper.MakeDynamicParamGE()
    //}


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

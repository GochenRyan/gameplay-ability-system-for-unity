using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class EnemyActor
{
    [NonSerialized, OdinSerialize]
    public int TID;
    [NonSerialized, OdinSerialize]
    public EnemyModel EnemyModel;

    public static event Action<EnemyActor> Created;
    public static event Action<EnemyActor> Destroy;

    public void CreateByTID(int tID)
    {
        if (DataManager.Instance.EnemyMap.ContainsKey(TID))
        {
            throw new Exception($"Enemy map does not have key: {tID}");
        }

        var enemyDefine = DataManager.Instance.EnemyMap[TID];

        List<string> dynamicEffects = new List<string>(enemyDefine.Effects);
        List<string> dynamicAbilities = new List<string>(enemyDefine.Abilities);

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
            Effects = dynamicEffects
        };
        AllEnemyActor.Add(EnemyModel.ID, this);
        Created?.Invoke(this);
    }

    public static int GenerateID()
    {
        var IDs = AllEnemyActor.Keys;
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
    public static Dictionary<int, EnemyActor> AllEnemyActor = new Dictionary<int, EnemyActor>();
}

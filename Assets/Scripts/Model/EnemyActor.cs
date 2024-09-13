using Sirenix.Serialization;
using System;

[Serializable]
public class EnemyActor
{
    [NonSerialized, OdinSerialize]
    public int TID;
    [NonSerialized, OdinSerialize]
    public EnemyModel Model;

    public void CreateByTID(int tID)
    {

    }

    public void CreateByModel(EnemyModel model)
    {

    }
}

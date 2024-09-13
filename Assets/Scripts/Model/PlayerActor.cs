using Sirenix.Serialization;
using System;

[Serializable]
public class PlayerActor
{
    [NonSerialized, OdinSerialize]
    public int TID;
    [NonSerialized, OdinSerialize]
    public PlayerModel PlayerModel;

    public void CreateByTID(int tID)
    {

    }

    public void CreateByModel(PlayerModel model)
    {

    }
}

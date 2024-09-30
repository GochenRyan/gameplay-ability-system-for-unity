using Sirenix.Serialization;
using System;
using System.Collections.Generic;

[Serializable]
public class GameActor
{
    public static GameActor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameActor();
            }
            return instance;
        }

    }

    public void Load(GameActor actor)
    {
        this.AllPlayerActor = actor.AllPlayerActor;
        foreach(var pair in AllPlayerActor)
        {
            PlayerCreated?.Invoke(pair.Value);
        }

        this.AllEnemyActor = actor.AllEnemyActor;
        foreach (var pair in AllEnemyActor)
        {
            EnemyCreated?.Invoke(pair.Value);
        }

        this.AllEquipmentActor = actor.AllEquipmentActor;
        foreach (var pair in AllEquipmentActor)
        {
            EquipmentCreated?.Invoke(pair.Value);
        }
    }

    public void DestroyAll()
    {
        foreach (var pair in AllPlayerActor)
        {
            PlayerDestroy?.Invoke(pair.Value);
        }
        AllPlayerActor.Clear();

        foreach (var pair in AllEnemyActor)
        {
            EnemyDestroy?.Invoke(pair.Value);
        }
        AllEnemyActor.Clear();

        foreach (var pair in AllEquipmentActor)
        {
            EquipmentDestroy?.Invoke(pair.Value);
        }
        AllEquipmentActor.Clear();
    }

    static GameActor instance;

    private GameActor()
    {
    }

    public void AddPlayer(PlayerActor playerActor)
    {
        AllPlayerActor.Add(playerActor.PlayerModel.ID, playerActor);
        PlayerCreated?.Invoke(playerActor);
    }

    public void DestroyPlayer(PlayerActor playerActor)
    {
        AllPlayerActor.Remove(playerActor.PlayerModel.ID);
        PlayerDestroy?.Invoke(playerActor);
    }

    public void AddEnemy(EnemyActor enemyActor)
    {
        AllEnemyActor.Add(enemyActor.EnemyModel.ID, enemyActor);
        EnemyCreated?.Invoke(enemyActor);
    }

    public void DestoryEnemy(EnemyActor enemyActor)
    {
        AllEnemyActor.Remove(enemyActor.EnemyModel.ID);
        EnemyDestroy?.Invoke(enemyActor);
    }

    public void AddEquipment(EquipmentActor equipActor)
    {
        AllEquipmentActor.Add(equipActor.EquipmentModel.ID, equipActor);
        EquipmentCreated?.Invoke(equipActor);
    }

    public void DestoryEquipment(EquipmentActor equipActor)
    {
        AllEquipmentActor.Remove(equipActor.EquipmentModel.ID);
        EquipmentDestroy?.Invoke(equipActor);
    }

    [NonSerialized, OdinSerialize]
    public Dictionary<int, PlayerActor> AllPlayerActor = new Dictionary<int, PlayerActor>();
    [NonSerialized, OdinSerialize]
    public Dictionary<int, EnemyActor> AllEnemyActor = new Dictionary<int, EnemyActor>();
    [NonSerialized, OdinSerialize]
    public Dictionary<int, EquipmentActor> AllEquipmentActor = new Dictionary<int, EquipmentActor>();

    public event Action<PlayerActor> PlayerCreated;
    public event Action<PlayerActor> PlayerDestroy;

    public event Action<EnemyActor> EnemyCreated;
    public event Action<EnemyActor> EnemyDestroy;

    public event Action<EquipmentActor> EquipmentCreated;
    public event Action<EquipmentActor> EquipmentDestroy;
}

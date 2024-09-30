using GAS;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameRunner : MonoBehaviour
{
    private bool _isRunning;
    public static GameRunner Instance { get; private set; }

    private PlayerInput _input;

    private void Awake()
    {
        SavePath = Path.Combine(Application.persistentDataPath + "save_test.bin");

        Instance = this;
        _input = new PlayerInput();
        _input.Enable();
        _input.Game.Save.performed += Save_performed;
        _input.Game.Load.performed += Load_performed;
    }

    private void Load_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        GameOver();

        byte[] bytes = File.ReadAllBytes(SavePath);
        var gameActor = SerializationUtility.DeserializeValue<GameActor>(bytes, DataFormat.Binary);
        GameActor.Instance.Load(gameActor);
        GameplayAbilitySystem.GAS.Unpause();
        _isRunning = true;
    }

    private void Save_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        byte[] bytes = SerializationUtility.SerializeValue(GameActor.Instance, DataFormat.Binary);
        File.WriteAllBytes(SavePath, bytes);
        GameOver();
        GameActor.Instance.DestroyAll();
    }

    private void Start()
    {
        WaitForFirstGameStart();
    }

    private void WaitForFirstGameStart()
    {
        var dataMnager = DataManager.Instance;
    }

    public void StartGame()
    {
        GameActor.Instance.EquipmentCreated += EquipmentActor_Created;
        GameActor.Instance.EquipmentDestroy += EquipmentActor_Destroy;

        GameActor.Instance.PlayerCreated += PlayerActor_Created;
        GameActor.Instance.PlayerDestroy += PlayerActor_Destroy;

        GameActor.Instance.EnemyCreated += EnemyActor_Created;
        GameActor.Instance.EnemyDestroy += EnemyActor_Destroy;

        GameplayAbilitySystem.GAS.Unpause();
        GameActor.Instance.DestroyAll();
        CreatePlayer();
        _isRunning = true;
    }


    public void GameOver()
    {
        _isRunning = false;
        GameActor.Instance.DestroyAll();
        GameplayAbilitySystem.GAS.Pause();
    }

    #region Player Management

    [SerializeField] private GameObject prefabPlayer;
    private Player _player;

    public Player Player
    {
        get { return _player; }
    }

    [SerializeField] private GameObject prefabEquipment;
    private Equipment _equipment;

    private void CreatePlayer()
    {
        if (_player != null) return;
        var actor = new PlayerActor();
        actor.CreateByTID(10001);
    }

    private void PlayerActor_Destroy(PlayerActor actor)
    {
        Destroy(_player.gameObject);
    }

    private void PlayerActor_Created(PlayerActor actor)
    {
        var go = Instantiate(prefabPlayer);
        go.transform.position = actor.PlayerModel.Position;
        _player = go.GetComponent<Player>();
        _player.Init(actor);
    }

    #endregion


    #region Enemy Managment

    [SerializeField] private GameObject prefabEnemy;
    private readonly List<Enemy> _enemies = new();

    public List<Enemy> Enemies
    {
        get
        {
            return _enemies;
        }
    }

    private void Update()
    {
        if (_isRunning)
        {
            if (_enemies.Count == 0)
            {
                SpawnEnemy();
            }

            if (_equipment == null)
            {
                SpawnEquipment();
            }
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void SpawnEnemy()
    {
        var actor = new EnemyActor();
        actor.CreateByTID(10001);
    }

    private void EnemyActor_Destroy(EnemyActor actor)
    {
        foreach (var enemy in Enemies)
        {
            if (enemy.Actor == actor)
            {
                Destroy(enemy.gameObject);
                break;
            }
        }
    }

    private void EnemyActor_Created(EnemyActor actor)
    {
        var go = Instantiate(prefabEnemy);
        go.transform.position = actor.EnemyModel.Position;
        var enemy = go.GetComponent<Enemy>();
        enemy.Init(actor, _player);
    }

    #endregion


    #region Equipment Managment
    private void SpawnEquipment()
    {
        var actor = new EquipmentActor();
        actor.CreateByTID(10001);
    }

    private void EquipmentActor_Destroy(EquipmentActor actor)
    {
        Destroy(_equipment.gameObject);
        _equipment = null;
    }

    private void EquipmentActor_Created(EquipmentActor actor)
    {
        if (_equipment != null)
            return;

        var go = Instantiate(prefabEquipment);
        go.transform.position = actor.EquipmentModel.Position;
        _equipment = go.GetComponent<Equipment>();
        _equipment.EquipmentActor = actor;
    }
    #endregion

    private string SavePath;
}

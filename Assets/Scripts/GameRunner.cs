using GAS;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : MonoBehaviour
{
    private bool _isRunning;
    public static GameRunner Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        WaitForFirstGameStart();
    }

    private void WaitForFirstGameStart()
    {
    }

    public void StartGame()
    {
        EquipmentActor.Created += EquipmentActor_Created;
        EquipmentActor.Destroy += EquipmentActor_Destroy;

        GameplayAbilitySystem.GAS.Unpause();
        DestroyPlayer();
        DestroyEnemies();
        CreatePlayer();
        _isRunning = true;
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
        go.transform.position = _equipmentSpawnPosition;
        _equipment = go.GetComponent<Equipment>();
        _equipment.EquipmentActor = actor;
    }

    public void GameOver()
    {
        _isRunning = false;
        GameplayAbilitySystem.GAS.Pause();
    }

    #region Player Management

    [SerializeField] private GameObject prefabPlayer;
    [SerializeField] private Vector3 _playerSpawnPosition = Vector3.zero;
    private Player _player;

    [SerializeField] private GameObject prefabEquipment;
    [SerializeField] private Vector3 _equipmentSpawnPosition = Vector3.zero;
    private Equipment _equipment;

    private void CreatePlayer()
    {
        if (_player != null) return;
        var go = Instantiate(prefabPlayer);
        go.transform.position = _playerSpawnPosition;
        _player = go.GetComponent<Player>();
        _player.Init();
    }

    private void DestroyPlayer()
    {
        if (_player == null) return;
        Destroy(_player.gameObject);
        _player = null;
    }

    #endregion


    #region Enemy Managment
    
    [SerializeField] private GameObject prefabEnemy;
    private readonly List<Enemy> _enemies = new();
    [SerializeField] private Rect enemySpawnRect;

    public List<Enemy> Enemies { get; }

    private void Update()
    {
        if (_isRunning)
        {
            if (_enemies.Count == 0)
            {
                SpawnEnemy();
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
        var position = new Vector3(
            Random.Range(enemySpawnRect.xMin, enemySpawnRect.xMax),
            Random.Range(enemySpawnRect.yMin, enemySpawnRect.yMax),
            0);
        var go = Instantiate(prefabEnemy);
        go.transform.position = position;
        var enemy = go.GetComponent<Enemy>();
        enemy.Init(_player);
    }

    private void DestroyEnemies()
    {
        foreach (var enemy in _enemies) Destroy(enemy.gameObject);
        _enemies.Clear();
    }

    #endregion
}

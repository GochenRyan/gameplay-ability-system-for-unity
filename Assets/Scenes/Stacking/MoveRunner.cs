using Assets.GAS.Runtime.Core;
using GAS;
using GAS.Runtime;
using Model;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MoveRunner : MonoBehaviour
{
    private bool _isRunning;
    public static MoveRunner Instance { get; private set; }

    private void Awake()
    {
        SavePath = Path.Combine(Application.persistentDataPath + "save_move_test.bin");

        Instance = this;

        Addressables.LoadAssetAsync<GameplayEffectAsset>("Buff_Move_AttackUp").Completed += (handle) =>
        {
            _effectAsset = handle.Result;
        };
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
        GameActor.Instance.ActorCreated += Actor_Created;
        GameActor.Instance.ActorDestoryed += Actor_Destoryed;

        GameplayAbilitySystem.GAS.Unpause();
        GameActor.Instance.DestroyAll();
        CreatePlayer();
        _isRunning = true;
    }

    public void MoveNext()
    {
        GASTick.Instance.NextMove(_currentMove, ++_currentMove);
    }

    private int _currentMove = 0;

    public void AddAttackUpBuff()
    {
        if (_effectAsset != null)
        {
            var gameplayEffect = new GameplayEffect(_effectAsset);
            _player.ASC.ApplyGameplayEffectToSelf(gameplayEffect);
        }
    }

    private void CreatePlayer()
    {
        if (_player != null) return;
        var actor = new PlayerActor(GameActor.Instance);
        actor.CreateByTID(10001);
    }

    private void Actor_Destoryed(Type type, Actor actor)
    {
        if (type == typeof(PlayerActor))
        {
            var playerActor = actor as PlayerActor;
            PlayerActor_Destroy(playerActor);
        }
    }

    private void Actor_Created(Type type, Actor actor)
    {
        if (type == typeof(PlayerActor))
        {
            var playerActor = actor as PlayerActor;
            PlayerActor_Created(playerActor);
        }
    }

    private void PlayerActor_Created(PlayerActor actor)
    {
        var go = Instantiate(prefabPlayer);
        go.transform.position = actor.PlayerModel.Position;
        _player = go.GetComponent<MovePlayer>();
        _player.Init(actor);
    }

    private void PlayerActor_Destroy(PlayerActor actor)
    {
        Destroy(_player.gameObject);
    }

    private string SavePath;

    [SerializeField] private GameObject prefabPlayer;
    private MovePlayer _player;

    private GameplayEffectAsset _effectAsset;
}

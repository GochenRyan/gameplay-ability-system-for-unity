using GAS.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IEquipable
{
    private const float AttackDistance = 2.5f;
    private Player _player;
    private Rigidbody2D _rb;
    private AbilitySystemComponent _asc;
    private EnemyActor _enemyActor;

    public AbilitySystemComponent ASC { get { return _asc; } }
    public EnemyActor Actor { get { return _enemyActor; } }

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _asc = GetComponent<AbilitySystemComponent>();
        GameRunner.Instance.RegisterEnemy(this);
    }

    private void OnDestroy()
    {
        GameRunner.Instance.UnregisterEnemy(this);
    }

    private void Update()
    {
        if (Chase()) Attack();

        if (_player != null)
        {
            var dir = (Vector2)(_player.transform.position - transform.position);
            dir.Normalize();
            transform.up = dir;
            _attackTimer += Time.deltaTime;
        }
    }

    public void Init(EnemyActor actor, Player player)
    {
        _player = player;
        _enemyActor = actor;
        _asc.InitWithPreset(1);
        InitAttribute(actor);
    }

    void InitAttribute(EnemyActor actor)
    {
        _asc.AttrSet<AS_Fight>().HP.RegisterPostCurrentValueChange(OnHpChange);
        _asc.AttrSet<AS_Fight>().Attack.RegisterPostCurrentValueChange(OnAttackChange);
        _asc.AttrSet<AS_Fight>().Defense.RegisterPostCurrentValueChange(OnDefenseChange);
        _asc.AttrSet<AS_Fight>().Speed.RegisterPostCurrentValueChange(OnSpeedChange);

        _asc.AttrSet<AS_Fight>().InitMaxHP(actor.EnemyModel.HP);
        _asc.AttrSet<AS_Fight>().InitHP(actor.EnemyModel.HP);
        _asc.AttrSet<AS_Fight>().InitAttack(actor.EnemyModel.Attack);
        _asc.AttrSet<AS_Fight>().InitDefense(actor.EnemyModel.Defense);
        _asc.AttrSet<AS_Fight>().InitSpeed(actor.EnemyModel.Speed);

        var abilities = actor.MakeAbilities();
        foreach(var ability in abilities)
        {
            var abilitySpec = _asc.GrantAbility(ability);
            if (ability.Tag.AssetTag.Tags.Contains(GTagLib.Ability_Skill_Active))
            {
                _activeAbility = ability.Name;
            }
            else if (ability.Tag.AssetTag.Tags.Contains(GTagLib.Ability_Skill_Passive))
            {
                _passiveAbilities.Add(ability.Name);
            }
        }

        var ges = actor.MakePlayerModelGE();
        foreach (var ge in ges)
        {
            _asc.ApplyGameplayEffectToSelf(ge);
        }

        _asc.AbilityContainer.AbilitySpecs()[GAbilityLib.Die.Name].RegisterEndAbility(OnDie);
    }

    void OnHpChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        UIManager.Instance.SetEnemyHP((int)newValue);

        if (newValue <= 0)
        {
            _asc.TryActivateAbility(GAbilityLib.Die.Name);
        }
    }

    void OnAttackChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        UIManager.Instance.SetEnemyAttack((int)newValue);
    }

    void OnDefenseChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        UIManager.Instance.SetEnemyDefense((int)newValue);
    }

    void OnSpeedChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        UIManager.Instance.SetEnemySpeed((int)newValue);
    }

    private void OnDie()
    {
        _enemyActor.Destroy();
    }

    private bool Chase()
    {
        _enemyActor.EnemyModel.Position = this.transform.position;
        if (_player == null)
        {
            _rb.velocity = Vector2.zero;
            return false;
        }

        var delta = (Vector2)(_player.transform.position - transform.position);

        var distance = delta.magnitude;
        if (distance < AttackDistance)
        {
            _rb.velocity = Vector2.zero;
            return true;
        }
        else
        {
            var speed = 5;
            _rb.velocity = delta.normalized * speed;
            return false;
        }
    }

    private void Attack()
    {
        if (_attackTimer < 1)
            return;
        _attackTimer = 0;
        _asc.TryActivateAbility(_activeAbility);
    }

    public void AddEquipment(EquipmentActor actor)
    {
        var ges = actor.MakeEquipmentModelGE();
        foreach(var ge in ges)
        {
            var geSpec = _asc.ApplyGameplayEffectToSelf(ge);
            _equipGESpecs.Add(geSpec);
        }
    }

    public void RemoveEquipment(EquipmentActor actor)
    {
        foreach(var geSpec in _equipGESpecs)
        {
            _asc.RemoveGameplayEffect(geSpec);
        }
        _equipGESpecs.Clear();
    }

    private List<GameplayEffectSpec> _equipGESpecs = new List<GameplayEffectSpec>();
    private string _activeAbility;
    private List<string> _passiveAbilities = new List<string>();
    private float _attackTimer = 0f;
}

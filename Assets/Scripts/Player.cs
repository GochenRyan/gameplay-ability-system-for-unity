using GAS.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IEquipable
{
    private Rigidbody2D _rb;
    private PlayerInput _input;
    private AbilitySystemComponent _asc;
    private PlayerActor _actor;

    public AbilitySystemComponent ASC { get { return _asc; } }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _asc = GetComponent<AbilitySystemComponent>();

        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Move.performed += OnActivateMove;
        _input.Player.Move.canceled += OnDeactivateMove;
        _input.Player.Attack.performed += Attack_performed;
        _input.Player.Skill.performed += Skill_performed;
    }

    private void Skill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _asc.TryActivateAbility(_activeAbility);
    }

    private void OnDestroy()
    {
        _input.Disable();
        _input.Player.Move.performed -= OnActivateMove;
        _input.Player.Move.canceled -= OnDeactivateMove;
        _input.Player.Attack.performed -= Attack_performed;
        _input.Player.Skill.performed -= Skill_performed;
    }

    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = (mousePos - transform.position);
        dir.z = 0;
        dir = dir.normalized;
        transform.up = dir;
    }

    public void Init(PlayerActor actor)
    {
        _actor = actor;
        _asc.InitWithPreset(1);
        InitAttribute(actor);
    }

    void InitAttribute(PlayerActor actor)
    {
        _asc.AttrSet<AS_Fight>().HP.RegisterPostCurrentValueChange(OnHpChange);
        _asc.AttrSet<AS_Fight>().Attack.RegisterPostCurrentValueChange(OnAttackChange);
        _asc.AttrSet<AS_Fight>().Defense.RegisterPostCurrentValueChange(OnDefenseChange);
        _asc.AttrSet<AS_Fight>().Speed.RegisterPostCurrentValueChange(OnSpeedChange);

        _asc.AttrSet<AS_Fight>().InitMaxHP(actor.PlayerModel.HP);
        _asc.AttrSet<AS_Fight>().InitHP(actor.PlayerModel.HP);
        _asc.AttrSet<AS_Fight>().InitAttack(actor.PlayerModel.Attack);
        _asc.AttrSet<AS_Fight>().InitDefense(actor.PlayerModel.Defense);
        _asc.AttrSet<AS_Fight>().InitSpeed(actor.PlayerModel.Speed);

        var abilities = actor.MakeAbilities();
        foreach (var ability in abilities)
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

        //_asc.AbilityContainer.AbilitySpecs()[GAbilityLib.Attack.Name].RegisterEndAbility(OnPerformPassiveSkills);
        _asc.AbilityContainer.AbilitySpecs()[GAbilityLib.Die.Name].RegisterEndAbility(OnDie);
    }

    void OnHpChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        UIManager.Instance.SetPlayerHp((int)newValue);

        if (newValue <= 0)
        {
            _asc.TryActivateAbility(GAbilityLib.Die.Name);
        }
    }

    void OnAttackChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        UIManager.Instance.SetPlayerAttack((int)newValue);
    }

    void OnDefenseChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        UIManager.Instance.SetPlayerDefense((int)newValue);
    }

    void OnSpeedChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        UIManager.Instance.SetPlayerSpeed((int)newValue);
    }

    //private void OnPerformPassiveSkills()
    //{
    //    foreach(var pair in _asc.AbilityContainer.AbilitySpecs())
    //    {
    //        string name = pair.Key;
    //        var skill = pair.Value;

    //        if (skill.Ability.Tag.AssetTag.Tags.Contains(GTagLib.Ability_Skill_Passive))
    //        {
    //            _asc.TryActivateAbility(name);
    //        }
    //    }
    //}

    private void OnDie()
    {
        GameRunner.Instance.GameOver();
        GameActor.Instance.DestroyPlayer(_actor);
    }


    void OnActivateMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        var move = context.ReadValue<Vector2>();
        var velocity = _rb.velocity;
        velocity.x = move.x;
        velocity.y = move.y;
        velocity = velocity.normalized * 5;
        _rb.velocity = velocity;
    }

    void OnDeactivateMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _rb.velocity = Vector2.zero;
    }

    public void AddEquipment(EquipmentActor actor)
    {
        var ges = actor.MakeEquipmentModelGE();
        foreach (var ge in ges)
        {
            var geSpec = _asc.ApplyGameplayEffectToSelf(ge);
            _equipGESpecs.Add(geSpec);
        }
    }

    public void RemoveEquipment(EquipmentActor actor)
    {
        foreach (var geSpec in _equipGESpecs)
        {
            _asc.RemoveGameplayEffect(geSpec);
        }
        _equipGESpecs.Clear();
    }

    private List<GameplayEffectSpec> _equipGESpecs = new List<GameplayEffectSpec>();
    private string _activeAbility;
    private List<string> _passiveAbilities = new List<string>();
}

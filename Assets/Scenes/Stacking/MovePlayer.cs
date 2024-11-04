using GAS.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private AbilitySystemComponent _asc;
    private PlayerActor _actor;

    public AbilitySystemComponent ASC { get { return _asc; } }

    void Awake()
    {
        _asc = GetComponent<AbilitySystemComponent>();
    }

    private void Skill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _asc.TryActivateAbility(_activeAbility);
    }

    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = (mousePos - transform.position);
        dir.z = 0;
        dir = dir.normalized;
        transform.up = dir;

        _actor.PlayerModel.Position = transform.position;
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
        MoveUI.Instance.SetPlayerHp((int)newValue);

        if (newValue <= 0)
        {
            _asc.TryActivateAbility(GAbilityLib.Die.Name);
        }
    }

    void OnAttackChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        MoveUI.Instance.SetPlayerAttack((int)newValue);
    }

    void OnDefenseChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        MoveUI.Instance.SetPlayerDefense((int)newValue);
    }

    void OnSpeedChange(AttributeBase attributeBase, float oldValue, float newValue)
    {
        MoveUI.Instance.SetPlayerSpeed((int)newValue);
    }

    private void OnDie()
    {
        GameRunner.Instance.GameOver();
        _actor.Destroy();
    }

    private string _activeAbility;
    private List<string> _passiveAbilities = new List<string>();
}

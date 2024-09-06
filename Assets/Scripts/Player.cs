using GAS.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInput _input;
    private AbilitySystemComponent _asc;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

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

    public void Init()
    {
        _asc.InitWithPreset(1);
        InitAttribute();
    }

    void InitAttribute()
    {
        _asc.AttrSet<AS_Fight>().InitHP(100);
        _asc.AttrSet<AS_Fight>().InitAttack(10);
        _asc.AttrSet<AS_Fight>().InitSpeed(8);

        //_asc.AttrSet<AS_Fight>().HP.RegisterPostBaseValueChange(OnHpChange);
        //_asc.AbilityContainer.AbilitySpecs()[ability.Die.Name].RegisterEndAbility(OnDie);
    }

    private void OnDie()
    {
        GameRunner.Instance.GameOver();
        Destroy(gameObject);
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
}

using GAS.Runtime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float AttackDistance = 2.5f;
    private Player _player;
    private Rigidbody2D _rb;
    private AbilitySystemComponent _asc;

    public AbilitySystemComponent ASC { get { return _asc; } }

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
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
        }
    }

    public void Init(Player player)
    {
        _player = player;
        _asc.InitWithPreset(1);
        InitAttribute();
    }

    void InitAttribute()
    {
        _asc.AttrSet<AS_Fight>().InitHP(50);
        _asc.AttrSet<AS_Fight>().InitAttack(20);
        _asc.AttrSet<AS_Fight>().InitDefense(10);
        _asc.AttrSet<AS_Fight>().InitSpeed(6);

        //_asc.AttrSet<AS_Fight>().HP.RegisterPostBaseValueChange(OnHpChange);
        _asc.AbilityContainer.AbilitySpecs()[GAbilityLib.Die.Name].RegisterEndAbility(OnDie);
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }

    private bool Chase()
    {
        if (_player == null)
        {
            _rb.velocity = Vector2.zero;
            return false;
        }

        var delta = (Vector2)(_player.transform.position - transform.position);
        var speed = 5;
        _rb.velocity = delta.normalized * speed;

        var distance = delta.magnitude;
        return distance < AttackDistance;
    }

    private void Attack()
    {
        _asc.TryActivateAbility(GAbilityLib.Attack.Name);
    }
}

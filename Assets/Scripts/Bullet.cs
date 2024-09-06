using GAS.Runtime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AbilitySystemComponent))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameplayEffectAsset geBulletDamage;
    private Rigidbody2D _rb;
    private AbilitySystemComponent _asc;
    private GameplayEffect _geBulletDamage;


    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _asc = gameObject.GetComponent<AbilitySystemComponent>();
        _geBulletDamage = new GameplayEffect(geBulletDamage);
    }

    public void Init(Vector2 position, Vector2 direction, float speed, float damage)
    {
        transform.position = position;
        _rb.velocity = direction * speed;

        // Set damage value
        _asc.InitWithPreset(1);
        _asc.AttrSet<AS_Bullet>().InitAttack(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out AbilitySystemComponent enemy))
        {
            if (_asc.HasTag(GTagLib.Faction_Player) && enemy.HasTag(GTagLib.Faction_Enemy) || 
                _asc.HasTag(GTagLib.Faction_Enemy) && enemy.HasTag(GTagLib.Faction_Player))
            {
                _asc.ApplyGameplayEffectTo(_geBulletDamage, enemy);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

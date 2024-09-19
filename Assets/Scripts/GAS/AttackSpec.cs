using GAS.Runtime;
using UnityEngine.AddressableAssets;

public class AttackSpec : AbilitySpec<Attack>
{
    public AttackSpec(Attack ability, AbilitySystemComponent owner) : base(ability, owner)
    {
        Addressables.LoadAssetAsync<GameplayEffectAsset>("GE_NormalDamage").Completed += (handle) =>
        {
            _effectAsset = handle.Result;
        };
    }

    public override void ActivateAbility(params object[] args)
    {
        if (_effectAsset != null)
        {

            if (Owner.GameplayTagAggregator.HasAnyTags(GTagLib.Faction_Player))
            {
                if (GameRunner.Instance.Enemies.Count > 0)
                {
                    var enemy = GameRunner.Instance.Enemies[0];
                    var gameplayEffect = new GameplayEffect(_effectAsset);
                    Owner.ApplyGameplayEffectTo(gameplayEffect, enemy.ASC);
                }
            }
            else
            {
                var player = GameRunner.Instance.Player;
                if (player != null)
                {
                    var gameplayEffect = new GameplayEffect(_effectAsset);
                    Owner.ApplyGameplayEffectTo(gameplayEffect, player.ASC);
                }
            }
            TryEndAbility();
        }
    }

    public override void CancelAbility()
    {
    }

    public override void EndAbility()
    {
    }

    private GameplayEffectAsset _effectAsset;
}

using Assets.GAS.Runtime.Core;
using GAS.General;
using System;
using UnityEngine;

namespace GAS
{
    public class GasHost : MonoBehaviour
    {
        private GameplayAbilitySystem _gas => GameplayAbilitySystem.GAS;

        private void OnEnable()
        {
            GASTick.Instance.TurnTick += OnTurnTick;
            GASTick.Instance.MoveTick += OnMoveTick;
        }

        private void OnMoveTick(object sender, MoveTickEvent e)
        {
            _gas.MoveTick(e);
        }

        private void OnTurnTick(object sender, TurnTickEvent e)
        {
            _gas.TurnTick(e);
        }

        private void Update()
        {
            GASTimer.UpdateCurrentFrameCount();
            _gas.Tick();
        }

        private void OnDestroy()
        {
            _gas.ClearComponents();
        }
    }
}
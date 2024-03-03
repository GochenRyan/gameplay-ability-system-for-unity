﻿using System;
using System.Collections.Generic;
using System.Linq;
using GAS.General;
using GAS.Runtime.Ability.TargetCatcher;
using GAS.Runtime.Effects;
using UnityEngine;

namespace GAS.Runtime.Ability.TimelineAbility
{
    [Serializable]
    public class ReleaseGameplayEffectTrackData : TrackDataBase
    {
        public List<ReleaseGameplayEffectMarkEvent> markEvents = new();
    }

    [Serializable]
    public class ReleaseGameplayEffectMarkEvent : MarkEventBase
    {
        //public LockOnTargetMethod method = new LockOnTargetMethod();
        public JsonData jsonTargetCatcher = new JsonData()
        {
            Type = typeof(CatchSelf).FullName // 默认 CatchSelf
        };
        public List<GameplayEffectAsset> gameplayEffectAssets = new();

        public void SaveTargetCatcher(TargetCatcherBase targetCatcher)
        {
            var jsonData = JsonUtility.ToJson(targetCatcher);
            var dataType = targetCatcher.GetType().FullName;
            jsonTargetCatcher = new JsonData
            {
                Type = dataType,
                Data = jsonData
            };
        }

        public TargetCatcherBase LoadTargetCatcher()
        {
            TargetCatcherBase targetCatcher = null;
            var jsonData = jsonTargetCatcher.Data;
            var dataType = jsonTargetCatcher.Type;

            var type = TargetCatcherSonTypes.FirstOrDefault(sonType => sonType.FullName == dataType);
            if (type == null)
                Debug.LogError("[EX] TargetCatcherBase SonType not found: " + dataType);
            else
            {
                if (string.IsNullOrEmpty(jsonData))
                {
                    targetCatcher = Activator.CreateInstance(type) as TargetCatcherBase;
                }else
                    targetCatcher = JsonUtility.FromJson(jsonData, type) as TargetCatcherBase;
            }

            return targetCatcher;
        }

        #region TargetCatcher SonTypes

        private static Type[] _targetCatcherSonTypes;

        public static Type[] TargetCatcherSonTypes =>
            _targetCatcherSonTypes ??= TypeUtil.GetAllSonTypesOf(typeof(TargetCatcherBase));

        #endregion
    }


    // public enum LockMethod
    // {
    //     Self,
    //     Circle2D,
    //     Box2D,
    //     Sphere3D,
    //     Box3D,
    //     Custom
    // }
    //
    // public enum CenterType
    // {
    //     Relative,
    //     WorldSpace
    // }
    //
    // [Serializable]
    // public class LockOnTargetMethod
    // {
    //     public LockMethod method;
    //
    //     // 检测碰撞
    //     public LayerMask checkLayer;
    //     public CenterType centerType;
    //
    //     public Vector3 center;
    //
    //     // Circle2D,Sphere3D
    //     public float radius;
    //
    //     // Box2D,Box3D
    //     public Vector3 size;
    //
    //     // Custom
    //     public string customMethodRegisterKey;
    // }
}
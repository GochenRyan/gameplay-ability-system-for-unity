///////////////////////////////////
//// This is a generated file. ////
////     Do not modify it.     ////
///////////////////////////////////

using System;
using System.Collections.Generic;

namespace GAS.Runtime
{
    public static class GAbilityLib
    {
        public struct AbilityInfo
        {
            public string Name;
            public string AssetPath;
            public Type AbilityClassType;
        }

        public static AbilityInfo Attack = new AbilityInfo { Name = "Attack", AssetPath = "Assets/Config/GAS/GameplayAbilityLib/Attack.asset",AbilityClassType = typeof(Attack) };

        public static AbilityInfo Die = new AbilityInfo { Name = "Die", AssetPath = "Assets/Config/GAS/GameplayAbilityLib/Die.asset",AbilityClassType = typeof(GAS.Runtime.TimelineAbility) };

        public static AbilityInfo Equip = new AbilityInfo { Name = "Equip", AssetPath = "Assets/Config/GAS/GameplayAbilityLib/Equip.asset",AbilityClassType = typeof(Equip) };


        public static Dictionary<string, AbilityInfo> AbilityMap = new Dictionary<string, AbilityInfo>
        {
            ["Attack"] = Attack,
            ["Die"] = Die,
            ["Equip"] = Equip,
        };
    }
}
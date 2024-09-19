///////////////////////////////////
//// This is a generated file. ////
////     Do not modify it.     ////
///////////////////////////////////

using System;
using System.Collections.Generic;

namespace GAS.Runtime
{
    public class AS_Equipment : AttributeSet
    {
        #region Attack

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase Attack { get; } = new ("AS_Equipment", "Attack", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitAttack(float value)
        {
            Attack.SetBaseValue(value);
            Attack.SetCurrentValue(value);
        }

        public void SetCurrentAttack(float value)
        {
            Attack.SetCurrentValue(value);
        }

        public void SetBaseAttack(float value)
        {
            Attack.SetBaseValue(value);
        }

        public void SetMinAttack(float value)
        {
            Attack.SetMinValue(value);
        }

        public void SetMaxAttack(float value)
        {
            Attack.SetMaxValue(value);
        }

        public void SetMinMaxAttack(float min, float max)
        {
            Attack.SetMinMaxValue(min, max);
        }

        #endregion Attack

        #region Defense

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase Defense { get; } = new ("AS_Equipment", "Defense", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitDefense(float value)
        {
            Defense.SetBaseValue(value);
            Defense.SetCurrentValue(value);
        }

        public void SetCurrentDefense(float value)
        {
            Defense.SetCurrentValue(value);
        }

        public void SetBaseDefense(float value)
        {
            Defense.SetBaseValue(value);
        }

        public void SetMinDefense(float value)
        {
            Defense.SetMinValue(value);
        }

        public void SetMaxDefense(float value)
        {
            Defense.SetMaxValue(value);
        }

        public void SetMinMaxDefense(float min, float max)
        {
            Defense.SetMinMaxValue(min, max);
        }

        #endregion Defense

        #region HP

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase HP { get; } = new ("AS_Equipment", "HP", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitHP(float value)
        {
            HP.SetBaseValue(value);
            HP.SetCurrentValue(value);
        }

        public void SetCurrentHP(float value)
        {
            HP.SetCurrentValue(value);
        }

        public void SetBaseHP(float value)
        {
            HP.SetBaseValue(value);
        }

        public void SetMinHP(float value)
        {
            HP.SetMinValue(value);
        }

        public void SetMaxHP(float value)
        {
            HP.SetMaxValue(value);
        }

        public void SetMinMaxHP(float min, float max)
        {
            HP.SetMinMaxValue(min, max);
        }

        #endregion HP

        #region Speed

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase Speed { get; } = new ("AS_Equipment", "Speed", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitSpeed(float value)
        {
            Speed.SetBaseValue(value);
            Speed.SetCurrentValue(value);
        }

        public void SetCurrentSpeed(float value)
        {
            Speed.SetCurrentValue(value);
        }

        public void SetBaseSpeed(float value)
        {
            Speed.SetBaseValue(value);
        }

        public void SetMinSpeed(float value)
        {
            Speed.SetMinValue(value);
        }

        public void SetMaxSpeed(float value)
        {
            Speed.SetMaxValue(value);
        }

        public void SetMinMaxSpeed(float min, float max)
        {
            Speed.SetMinMaxValue(min, max);
        }

        #endregion Speed

        public override AttributeBase this[string key]
        {
            get
            {
                switch (key)
                {
                    case "HP":
                        return HP;
                    case "Speed":
                        return Speed;
                    case "Attack":
                        return Attack;
                    case "Defense":
                        return Defense;
                }

                return null;
            }
        }

        public override string[] AttributeNames { get; } =
        {
            "HP",
            "Speed",
            "Attack",
            "Defense",
        };

        public override void SetOwner(AbilitySystemComponent owner)
        {
            _owner = owner;
            HP.SetOwner(owner);
            Speed.SetOwner(owner);
            Attack.SetOwner(owner);
            Defense.SetOwner(owner);
        }

        public static class Lookup
        {
            public const string HP = "AS_Equipment.HP";
            public const string Speed = "AS_Equipment.Speed";
            public const string Attack = "AS_Equipment.Attack";
            public const string Defense = "AS_Equipment.Defense";
        }
    }

    public class AS_Fight : AttributeSet
    {
        #region Attack

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase Attack { get; } = new ("AS_Fight", "Attack", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitAttack(float value)
        {
            Attack.SetBaseValue(value);
            Attack.SetCurrentValue(value);
        }

        public void SetCurrentAttack(float value)
        {
            Attack.SetCurrentValue(value);
        }

        public void SetBaseAttack(float value)
        {
            Attack.SetBaseValue(value);
        }

        public void SetMinAttack(float value)
        {
            Attack.SetMinValue(value);
        }

        public void SetMaxAttack(float value)
        {
            Attack.SetMaxValue(value);
        }

        public void SetMinMaxAttack(float min, float max)
        {
            Attack.SetMinMaxValue(min, max);
        }

        #endregion Attack

        #region Defense

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase Defense { get; } = new ("AS_Fight", "Defense", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitDefense(float value)
        {
            Defense.SetBaseValue(value);
            Defense.SetCurrentValue(value);
        }

        public void SetCurrentDefense(float value)
        {
            Defense.SetCurrentValue(value);
        }

        public void SetBaseDefense(float value)
        {
            Defense.SetBaseValue(value);
        }

        public void SetMinDefense(float value)
        {
            Defense.SetMinValue(value);
        }

        public void SetMaxDefense(float value)
        {
            Defense.SetMaxValue(value);
        }

        public void SetMinMaxDefense(float min, float max)
        {
            Defense.SetMinMaxValue(min, max);
        }

        #endregion Defense

        #region HP

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase HP { get; } = new ("AS_Fight", "HP", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitHP(float value)
        {
            HP.SetBaseValue(value);
            HP.SetCurrentValue(value);
        }

        public void SetCurrentHP(float value)
        {
            HP.SetCurrentValue(value);
        }

        public void SetBaseHP(float value)
        {
            HP.SetBaseValue(value);
        }

        public void SetMinHP(float value)
        {
            HP.SetMinValue(value);
        }

        public void SetMaxHP(float value)
        {
            HP.SetMaxValue(value);
        }

        public void SetMinMaxHP(float min, float max)
        {
            HP.SetMinMaxValue(min, max);
        }

        #endregion HP

        #region MaxHP

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase MaxHP { get; } = new ("AS_Fight", "MaxHP", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitMaxHP(float value)
        {
            MaxHP.SetBaseValue(value);
            MaxHP.SetCurrentValue(value);
        }

        public void SetCurrentMaxHP(float value)
        {
            MaxHP.SetCurrentValue(value);
        }

        public void SetBaseMaxHP(float value)
        {
            MaxHP.SetBaseValue(value);
        }

        public void SetMinMaxHP(float value)
        {
            MaxHP.SetMinValue(value);
        }

        public void SetMaxMaxHP(float value)
        {
            MaxHP.SetMaxValue(value);
        }

        public void SetMinMaxMaxHP(float min, float max)
        {
            MaxHP.SetMinMaxValue(min, max);
        }

        #endregion MaxHP

        #region Speed

        /// <summary>
        /// 
        /// </summary>
        public AttributeBase Speed { get; } = new ("AS_Fight", "Speed", 0f, CalculateMode.Stacking, (SupportedOperation)31, float.MinValue, float.MaxValue);

        public void InitSpeed(float value)
        {
            Speed.SetBaseValue(value);
            Speed.SetCurrentValue(value);
        }

        public void SetCurrentSpeed(float value)
        {
            Speed.SetCurrentValue(value);
        }

        public void SetBaseSpeed(float value)
        {
            Speed.SetBaseValue(value);
        }

        public void SetMinSpeed(float value)
        {
            Speed.SetMinValue(value);
        }

        public void SetMaxSpeed(float value)
        {
            Speed.SetMaxValue(value);
        }

        public void SetMinMaxSpeed(float min, float max)
        {
            Speed.SetMinMaxValue(min, max);
        }

        #endregion Speed

        public override AttributeBase this[string key]
        {
            get
            {
                switch (key)
                {
                    case "HP":
                        return HP;
                    case "Speed":
                        return Speed;
                    case "Attack":
                        return Attack;
                    case "Defense":
                        return Defense;
                    case "MaxHP":
                        return MaxHP;
                }

                return null;
            }
        }

        public override string[] AttributeNames { get; } =
        {
            "HP",
            "Speed",
            "Attack",
            "Defense",
            "MaxHP",
        };

        public override void SetOwner(AbilitySystemComponent owner)
        {
            _owner = owner;
            HP.SetOwner(owner);
            Speed.SetOwner(owner);
            Attack.SetOwner(owner);
            Defense.SetOwner(owner);
            MaxHP.SetOwner(owner);
        }

        public static class Lookup
        {
            public const string HP = "AS_Fight.HP";
            public const string Speed = "AS_Fight.Speed";
            public const string Attack = "AS_Fight.Attack";
            public const string Defense = "AS_Fight.Defense";
            public const string MaxHP = "AS_Fight.MaxHP";
        }
    }

    public static class GAttrSetLib
    {
        public static readonly Dictionary<string, Type> AttrSetTypeDict = new Dictionary<string, Type>()
        {
            { "Fight", typeof(AS_Fight) },
            { "Equipment", typeof(AS_Equipment) },
        };

        public static readonly Dictionary<Type, string> TypeToName = new Dictionary<Type, string>
        {
            { typeof(AS_Fight), nameof(AS_Fight) },
            { typeof(AS_Equipment), nameof(AS_Equipment) },
        };

        public static List<string> AttributeFullNames = new List<string>()
        {
            "AS_Fight.HP",
            "AS_Fight.Speed",
            "AS_Fight.Attack",
            "AS_Fight.Defense",
            "AS_Fight.MaxHP",
            "AS_Equipment.HP",
            "AS_Equipment.Speed",
            "AS_Equipment.Attack",
            "AS_Equipment.Defense",
        };
    }
}
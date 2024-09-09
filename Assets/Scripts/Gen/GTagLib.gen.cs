///////////////////////////////////
//// This is a generated file. ////
////     Do not modify it.     ////
///////////////////////////////////

using System.Collections.Generic;

namespace GAS.Runtime
{
    public static class GTagLib
    {
        public static GameplayTag Ability { get; } = new GameplayTag("Ability");
        public static GameplayTag Ability_Attack { get; } = new GameplayTag("Ability.Attack");
        public static GameplayTag Ability_Die { get; } = new GameplayTag("Ability.Die");
        public static GameplayTag Ability_Equip { get; } = new GameplayTag("Ability.Equip");
        public static GameplayTag CD { get; } = new GameplayTag("CD");
        public static GameplayTag Event { get; } = new GameplayTag("Event");
        public static GameplayTag Event_Ban { get; } = new GameplayTag("Event.Ban");
        public static GameplayTag Event_Ban_Move { get; } = new GameplayTag("Event.Ban.Move");
        public static GameplayTag Event_Moving { get; } = new GameplayTag("Event.Moving");
        public static GameplayTag Faction { get; } = new GameplayTag("Faction");
        public static GameplayTag Faction_Enemy { get; } = new GameplayTag("Faction.Enemy");
        public static GameplayTag Faction_Player { get; } = new GameplayTag("Faction.Player");
        public static GameplayTag State { get; } = new GameplayTag("State");
        public static GameplayTag State_Buff { get; } = new GameplayTag("State.Buff");
        public static GameplayTag State_Buff_AttackUp { get; } = new GameplayTag("State.Buff.AttackUp");
        public static GameplayTag State_Debuff { get; } = new GameplayTag("State.Debuff");
        public static GameplayTag State_Debuff_Cold { get; } = new GameplayTag("State.Debuff.Cold");
        public static GameplayTag State_Debuff_Fire { get; } = new GameplayTag("State.Debuff.Fire");

        public static Dictionary<string, GameplayTag> TagMap = new Dictionary<string, GameplayTag>
        {
            ["Ability"] = Ability,
            ["Ability.Attack"] = Ability_Attack,
            ["Ability.Die"] = Ability_Die,
            ["Ability.Equip"] = Ability_Equip,
            ["CD"] = CD,
            ["Event"] = Event,
            ["Event.Ban"] = Event_Ban,
            ["Event.Ban.Move"] = Event_Ban_Move,
            ["Event.Moving"] = Event_Moving,
            ["Faction"] = Faction,
            ["Faction.Enemy"] = Faction_Enemy,
            ["Faction.Player"] = Faction_Player,
            ["State"] = State,
            ["State.Buff"] = State_Buff,
            ["State.Buff.AttackUp"] = State_Buff_AttackUp,
            ["State.Debuff"] = State_Debuff,
            ["State.Debuff.Cold"] = State_Debuff_Cold,
            ["State.Debuff.Fire"] = State_Debuff_Fire,
        };
    }
}
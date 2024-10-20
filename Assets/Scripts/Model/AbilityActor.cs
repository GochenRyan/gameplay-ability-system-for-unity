using Sirenix.Serialization;
using System;

namespace Model
{
    /// <summary>
    /// Completely relies on Ability resources for initialization, only records dynamic data.
    /// </summary>
    public class AbilityActor : Actor
    {
        public AbilityActor(Actor actor) : base(actor) { }

        [NonSerialized, OdinSerialize]
        public AbilityModel AbilityModel;
    }
}

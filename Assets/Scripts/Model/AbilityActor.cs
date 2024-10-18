using Sirenix.Serialization;
using System;

namespace Model
{
    /// <summary>
    /// Completely relies on Ability resources for initialization, only records dynamic data.
    /// </summary>
    public class AbilityActor
    {
        [NonSerialized, OdinSerialize]
        public AbilityModel AbilityModel;
    }
}

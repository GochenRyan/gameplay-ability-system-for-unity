using Sirenix.Serialization;
using System;

namespace Model
{
    /// <summary>
    /// Completely relies on effect resources for initialization, only records dynamic data.
    /// </summary>
    public class EffectActor : Actor
    {
        public EffectActor(Actor parent) : base(parent)  { }

        [NonSerialized, OdinSerialize]
        public EffectModel EffectModel;
    }
}

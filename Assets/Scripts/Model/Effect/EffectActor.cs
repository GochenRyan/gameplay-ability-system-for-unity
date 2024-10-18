using Sirenix.Serialization;
using System;

namespace Model
{
    /// <summary>
    /// Completely relies on effect resources for initialization, only records dynamic data.
    /// </summary>
    public class EffectActor
    {
        [NonSerialized, OdinSerialize]
        public EffectModel EffectModel;
    }
}

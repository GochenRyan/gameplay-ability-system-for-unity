using Sirenix.Serialization;
using System;
using System.Collections.Generic;

namespace Model
{
    public class ASCActor : Actor
    {
        public ASCActor(Actor actor) : base(actor) { }

        [NonSerialized, OdinSerialize]
        public ASCModel ASCModel;

        [NonSerialized, OdinSerialize]
        public Dictionary<string, AbilityActor> AbilityActors;

        [NonSerialized, OdinSerialize]
        public Dictionary<string, IList<EffectActor>> EffectActors;
    }
}

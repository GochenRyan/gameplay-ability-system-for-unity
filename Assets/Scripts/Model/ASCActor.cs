using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

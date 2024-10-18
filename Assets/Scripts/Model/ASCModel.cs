using System.Collections.Generic;

namespace Model
{
    public class ASCModel
    {
        // Find init data according to preset name -> attribute Sets, fixed tags, fixed abilities
        public string PresetName;
        public int Level;
        public Dictionary<string, float> Attributes;
    }
}

using System.Collections.Generic;

public class EnemyModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public float MaxHP { get; set; }
    public float HP { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float Speed { get; set; }
    public List<string> DynamicEffects { get; set; }
    public List<string> DynamicAbilities { get; set; }

}

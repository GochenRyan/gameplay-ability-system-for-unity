using System.Collections.Generic;

public class EquipmentModel
{
    public int ID { get; set; }
    public ItemAttributeDefine MainAttribute { get; set; }
    public List<ItemAttributeDefine> SecondaryAttributes { get; set; }
    public List<string> DynamicEffects { get; set; }
    public List<string> DynamicAbilities { get; set; }
}

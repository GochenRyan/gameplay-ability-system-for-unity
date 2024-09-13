using System.Collections.Generic;

public class EquipmentModel
{
    public int ID;
    public int TID;
    public ItemAttributeDefine MainAttribute;
    public List<ItemAttributeDefine> SecondaryAttributes;
    public List<string> DynamicEffects;
    public List<string> DynamicAbilities;
}

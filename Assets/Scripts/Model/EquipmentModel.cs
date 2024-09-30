using System.Collections.Generic;
using UnityEngine;

public class EquipmentModel
{
    public int ID;
    public int TID;
    public ItemAttributeDefine MainAttribute;
    public List<ItemAttributeDefine> SecondaryAttributes;
    public List<string> DynamicEffects;
    public List<string> DynamicAbilities;

    public Vector2 Position;
}

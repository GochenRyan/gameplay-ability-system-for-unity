using GAS.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentActor
{
    public EquipmentModel EquipmentModel;
    public List<GameplayEffect> MakeEquipmentModelGE()
    {
        var ges = new List<GameplayEffect>();

        var mainGEData = GetEquipAttrAddGE(EquipmentModel.MainAttribute);
        ges.Add(mainGEData);

        foreach (var attr in EquipmentModel.SecondaryAttributes)
        {
            var secondaryGEData = GetEquipAttrMulGE(attr);
            ges.Add(secondaryGEData);
        }

        return ges;
    }

    private GameplayEffect GetEquipAttrAddGE(ItemAttributeDefine itemAttributeDefine)
    {
        var geData = new InfiniteGameplayEffectData(EquipmentModel.ID.ToString() + itemAttributeDefine.Attribute.Name, 0);

        var attrMMC = ScriptableObject.CreateInstance<AttributeBasedModCalculation>();
        attrMMC.attributeName = itemAttributeDefine.Attribute.Name;
        attrMMC.captureType = AttributeBasedModCalculation.GEAttributeCaptureType.SnapShot;
        attrMMC.attributeFromType = AttributeBasedModCalculation.AttributeFrom.Source;
        attrMMC.k = 1;
        attrMMC.b = 0;

        var modifier = new GameplayEffectModifier
        {
            AttributeName = itemAttributeDefine.Attribute.Name,
            Operation = GEOperation.Add,
            MMC = attrMMC
        };
        geData.Modifiers.Append(modifier);

        var ge = new GameplayEffect(geData);

        return ge;
    }

    private GameplayEffect GetEquipAttrMulGE(ItemAttributeDefine itemAttributeDefine)
    {
        var geData = new InfiniteGameplayEffectData(EquipmentModel.ID.ToString() + itemAttributeDefine.Attribute.Name, 0);

        var attrMMC = ScriptableObject.CreateInstance<AttributeBasedModCalculation>();
        attrMMC.attributeName = itemAttributeDefine.Attribute.Name;
        attrMMC.captureType = AttributeBasedModCalculation.GEAttributeCaptureType.SnapShot;
        attrMMC.attributeFromType = AttributeBasedModCalculation.AttributeFrom.Source;
        attrMMC.k = 1;
        attrMMC.b = 1;

        var modifier = new GameplayEffectModifier
        {
            AttributeName = itemAttributeDefine.Attribute.Name,
            Operation = GEOperation.Multiply,
            MMC = attrMMC
        };
        geData.Modifiers.Append(modifier);

        var ge = new GameplayEffect(geData);

        return ge;
    }
}
﻿using GAS.Runtime;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EquipmentActor
{
    [NonSerialized, OdinSerialize]
    public int TID;
    [NonSerialized, OdinSerialize]
    public EquipmentModel EquipmentModel;

    public static event Action<EquipmentActor> Created;
    public static event Action<EquipmentActor> Destroy;

    public void CreateByTID(int tID)
    {
        if (!DataManager.Instance.EquipmentMap.ContainsKey(tID))
        {
            throw new Exception($"Equipment map does not have key: {tID}");
        }

        var equipmentDefine = DataManager.Instance.EquipmentMap[tID];
        var secondaryAttrs = new List<ItemAttributeDefine>();
        foreach(var attr in equipmentDefine.SecondaryAttrs)
        {
            secondaryAttrs.Add(new ItemAttributeDefine
            {
                AttributeName = attr.AttributeName,
                AttributeValue = attr.AttributeValue,
                Modifier = attr.Modifier
            });
        }

        List<string> dynamicEffects = new List<string>(equipmentDefine.Effects);
        List<string> dynamicAbilities = new List<string>(equipmentDefine.Abilities);

        EquipmentModel = new EquipmentModel
        {
            ID = GenerateID(),
            MainAttribute = new ItemAttributeDefine
            {
                AttributeName = equipmentDefine.MainAttr.AttributeName,
                AttributeValue = equipmentDefine.MainAttr.AttributeValue,
                Modifier = equipmentDefine.MainAttr.Modifier
            },
            SecondaryAttributes = secondaryAttrs,
            DynamicAbilities = dynamicAbilities,
            DynamicEffects = dynamicEffects
        };
        AllEquipmentActor.Add(EquipmentModel.ID, this);
        Created?.Invoke(this);
    }

    public void DestroyActor()
    {
        AllEquipmentActor.Remove(EquipmentModel.ID);
        Destroy?.Invoke(this);
    }
    
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
        var geData = new InfiniteGameplayEffectData(EquipmentModel.ID.ToString() + itemAttributeDefine.AttributeName, 0);

        var attrMMC = ScriptableObject.CreateInstance<AttributeBasedModCalculation>();
        attrMMC.attributeName = itemAttributeDefine.AttributeName;
        attrMMC.captureType = AttributeBasedModCalculation.GEAttributeCaptureType.SnapShot;
        attrMMC.attributeFromType = AttributeBasedModCalculation.AttributeFrom.Source;
        attrMMC.k = 1;
        attrMMC.b = 0;

        var modifier = new GameplayEffectModifier
        {
            AttributeName = itemAttributeDefine.AttributeName,
            Operation = GEOperation.Add,
            MMC = attrMMC
        };
        geData.Modifiers.Append(modifier);

        var ge = new GameplayEffect(geData);

        return ge;
    }

    private GameplayEffect GetEquipAttrMulGE(ItemAttributeDefine itemAttributeDefine)
    {
        var geData = new InfiniteGameplayEffectData(EquipmentModel.ID.ToString() + itemAttributeDefine.AttributeName, 0);

        var attrMMC = ScriptableObject.CreateInstance<AttributeBasedModCalculation>();
        attrMMC.attributeName = itemAttributeDefine.AttributeName;
        attrMMC.captureType = AttributeBasedModCalculation.GEAttributeCaptureType.SnapShot;
        attrMMC.attributeFromType = AttributeBasedModCalculation.AttributeFrom.Source;
        attrMMC.k = 1;
        attrMMC.b = 1;

        var modifier = new GameplayEffectModifier
        {
            AttributeName = itemAttributeDefine.AttributeName,
            Operation = GEOperation.Multiply,
            MMC = attrMMC
        };
        geData.Modifiers.Append(modifier);

        var ge = new GameplayEffect(geData);

        return ge;
    }

    public static int GenerateID()
    {
        var IDs = AllEquipmentActor.Keys;
        if (IDs.Count == 0 )
        {
            return 1;
        }
        else
        {
            return IDs.Max() + 1;
        }
    }

    [NonSerialized, OdinSerialize]
    public static Dictionary<int, EquipmentActor> AllEquipmentActor = new Dictionary<int, EquipmentActor>();
}
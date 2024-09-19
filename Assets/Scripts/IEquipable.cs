using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    void AddEquipment(EquipmentActor actor);
    void RemoveEquipment(EquipmentActor actor);
}

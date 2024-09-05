using UnityEngine;

[CreateAssetMenu(fileName = "CombatUnitData", menuName = "ScriptableObjects/CombatUnitSO", order = 1)]
public class CombatUnitSO : ScriptableObject
{
    public string UnitName;
    public int speed;
    public int MaxBaseHP;
    public int BaseAttack;
    public int BaseDefense;
}

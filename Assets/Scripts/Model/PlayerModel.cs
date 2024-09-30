using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int ID;
    public int TID;

    public float MaxHP;
    public float HP;
    public float Attack;
    public float Defense;
    public float Speed;

    public Vector2 Position;

    public List<string> Effects;
    public List<string> Abilities;
}

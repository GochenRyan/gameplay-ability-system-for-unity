using System.Collections.Generic;

public class PlayerDefine
{
    public int TID;
    public int HP;
    public int Attack;
    public int Defense;
    public int Speed;
    /// <summary>
    /// TODO
    ///     
    ///         1. 传入GE名字，直接从gas asset中获取GE
    ///         2. 参数会变化，传入GE名字和参数k,b
    ///         
    /// </summary>
    public List<string> Effects;
    public List<string> Abilities;
}

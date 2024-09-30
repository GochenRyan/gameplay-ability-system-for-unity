using System.Collections.Generic;

public class DataManager
{
    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager();
            }
            return instance;
        }

    }

    private DataManager()
    {
        CreateFakeData();
    }

    private void CreateFakeData()
    {
        EnemyMap.Add(10001, new EnemyDefine
        {
            TID = 10001,
            HP = 50,
            Attack = 30,
            Defense = 10,
            Speed = 10,
            Abilities = new List<string>{ "Attack" },
            Effects = new List<string>{ "Buff_AttackUp" }
        });

        PlayerMap.Add(10001, new PlayerDefine
        {
            TID = 10001,
            HP = 1000,
            Attack = 30,
            Defense = 15,
            Speed = 20,
            Abilities = new List<string>{ "Attack"},
            Effects = new List<string>{ "Buff_AttackUp" }
        });

        EquipmentMap.Add(10001, new EquipmentDefine
        {
            TID = 10001,
            MainAttr = new ItemAttributeDefine
            {
                AttributeName = "AS_Fight.Attack",
                AttributeValue = 5,
                Modifier = ModifierOp.Additive
            },
            SecondaryAttrs = new List<ItemAttributeDefine>
            {
                new ItemAttributeDefine
                {
                    AttributeName = "AS_Fight.Defense",
                    AttributeValue = 0.1f,
                    Modifier = ModifierOp.Multiply
                }
            },
            Abilities = new List<string>(),
            Effects = new List<string>{ "Buff_AttackUp" }
        });
    }

    public Dictionary<int, EnemyDefine> EnemyMap = new Dictionary<int, EnemyDefine>();
    public Dictionary<int, PlayerDefine> PlayerMap = new Dictionary<int, PlayerDefine>();
    public Dictionary<int, EquipmentDefine> EquipmentMap = new Dictionary<int, EquipmentDefine>();
}

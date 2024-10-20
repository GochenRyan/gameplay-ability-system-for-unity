using Model;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;

[Serializable]
public class GameActor : Actor
{
    public static GameActor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameActor(null);
            }
            return instance;
        }
    }

    private GameActor(Actor parent) : base(parent)  { }

    public void Load(GameActor actor)
    {
        foreach(var pair in Type2Children)
        {
            var Children = pair.Value;
            foreach (var child in Children)
            {
                child.Create();
            }
        }
    }

    public void DestroyAll()
    {
        foreach (var pair in Type2Children)
        {
            var Children = pair.Value;
            foreach (var child in Children)
            {
                child.Destroy();
            }
        }

        Actors.Clear();
    }

    static GameActor instance;


    [NonSerialized, OdinSerialize]
    public Dictionary<Type, List<Actor>> Actors = new Dictionary<Type, List<Actor>>();

    public Action<Type, Actor> ActorCreated;
    public Action<Type, Actor> ActorDestoryed;
}

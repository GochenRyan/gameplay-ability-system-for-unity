using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Actor
    {
        public static GameActor GameActor => GameActor.Instance;

        public Dictionary<Type, List<Actor>> Type2Children = new Dictionary<Type, List<Actor>>();
        public Actor Parent;

        public Actor(Actor parent) 
        {
            if (this is GameActor)
                return;

            Type type = this.GetType();
            if (!GameActor.Actors.ContainsKey(type))
            {
                GameActor.Actors.Add(type, new List<Actor>());
            }

            GameActor.Actors[type].Add(this);
            this.Parent = parent;

            if (!parent.Type2Children.ContainsKey(type))
            {
                parent.Type2Children[type] = new List<Actor>();
            }

            parent.Type2Children[type].Add(this);
        }

        public virtual void Create()
        {
            GameActor.ActorCreated?.Invoke(this.GetType(), this);
        }

        public virtual void Destroy() 
        {
            var type = this.GetType();
            if (Parent != null)
            {
                if (Parent.Type2Children.ContainsKey(type))
                {
                    Parent.Type2Children[type].Remove(this);
                    this.Parent = null;
                }
            }

            if (this is GameActor)
                return;
            GameActor.Actors[type].Remove(this);
            GameActor.ActorDestoryed?.Invoke(type, this);
        }

        protected virtual int GenerateID()
        {
            var type = this.GetType();
            if (!GameActor.Actors.ContainsKey(type))
            {
                return 1;
            }
            else
            {
                return GameActor.Actors.Count + 1;
            }
        }
    }
}

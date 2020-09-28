
namespace FizX.Engine
{
    public class Component
    {
        public GameObject GameObject { get; private set; }

        internal void SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public virtual void OnStart() { }

        public virtual void OnUpdate() { }

        public virtual void OnPause() { }

        public virtual void OnDestroy() { }
    }
}
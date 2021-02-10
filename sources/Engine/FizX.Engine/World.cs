using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FizX.Engine
{
    public class World
    {

        private Dictionary<Guid, GameObject> _gameObjects = new Dictionary<Guid, GameObject>();

        public IEnumerable<GameObject> GameObjects => _gameObjects.Values.AsEnumerable();

        private readonly ConcurrentBag<GameObject> _gameObjectsTrashBin = new ConcurrentBag<GameObject>();

        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject.Id, gameObject);
        }

        public void Start()
        {
            Parallel.ForEach(GameObjects, gameObject => { gameObject.Start(); });
        }

        public void Update(int elapsed)
        {
            Parallel.ForEach(GameObjects, gameObject =>
            {
                gameObject.Update(elapsed);
                if (gameObject.IsDestroyed)
                    _gameObjectsTrashBin.Add(gameObject);
            });
            Cleanup();
        }

        private void Cleanup()
        {
            foreach (var gameObject in _gameObjectsTrashBin)
                _gameObjects.Remove(gameObject.Id);
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Services.Pool
{
    [Serializable]
    public class Pool<T> where T : MonoBehaviour
    {
        private int _startCount;

        protected T Prefab { get; private set; }
        protected Stack<T> Stack { get; private set; } = new ();

        public Pool(T prefab, int startCount)
        {
            Prefab = prefab;
            _startCount = startCount;

            CreateStartCount();
        }

        public void Release(T template)
        {
            template.gameObject.SetActive(false); 
            Stack.Push(template);
        }

        public T Get()
        {
            if (Stack.TryPop(out T template) == false)
            {
                Stack.Push(Create());
                template = Stack.Pop();
            }
        
            template.gameObject.SetActive(true);

            return template;
        }

        protected T Create()
        {
            var enemy = Object.Instantiate(Prefab);
            enemy.gameObject.SetActive(false);

            return enemy;
        }
        
        private void CreateStartCount()
        {
            for (int i = 0; i < _startCount; i++)
            {
                Create();
            }
        }
    }
    
    [Serializable]
    public class Spawner<T> where T : MonoBehaviour, IDestoyable<T>
    {
        protected T Prefab { get; private set; }
        protected Pool<T> Pool { get; private set; }

        private List<T> _spawned = new();
        private Transform _parent;
        
        [field: SerializeField] protected int StartAmount { get; private set; } = 0;

        public Spawner(T prefab, Transform parent = null)
        {
            Prefab = prefab;
            Pool = new Pool<T>(prefab, StartAmount);
            _parent = parent;
        }

        public void DisableSpawned()
        {
            for (int i = _spawned.Count - 1; i >= 0; i--)
            {
                _spawned[i].Die();
            }
        }

        public T Spawn()
        {
            T spawnedObject = Pool.Get();

            spawnedObject.Destroyed += OnDestroyed;
            _spawned.Add(spawnedObject);
            
            if(_parent != null)
                spawnedObject.transform.SetParent(_parent);
            
            return spawnedObject;
        }

        protected void OnDestroyed(T spawnableObject)
        {
            spawnableObject.Destroyed -= OnDestroyed;
            _spawned.Remove(spawnableObject);

            Pool.Release(spawnableObject);
        }
    }
}
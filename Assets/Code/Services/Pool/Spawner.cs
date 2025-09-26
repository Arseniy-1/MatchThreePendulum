using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.Pool
{
    [Serializable]
    public class Spawner<T> where T : MonoBehaviour, IDestoyable<T>
    {
        protected Pool<T> Pool { get; private set; }

        private List<T> _spawned = new();
        private Transform _parent;
        
        [field: SerializeField] protected int StartAmount { get; private set; } = 0;

        public Spawner(Transform parent = null)
        {
            Pool = new Pool<T>(StartAmount);
            _parent = parent;
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
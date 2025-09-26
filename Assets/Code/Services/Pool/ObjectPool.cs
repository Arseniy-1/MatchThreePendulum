﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Services.Pool
{
    [Serializable]
    public class Pool<T> where T : MonoBehaviour
    {
        private int _startCount;

        protected Stack<T> Stack { get; private set; } = new ();

        public Pool(int startCount)
        {
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        private readonly Stack<T> _pool;
        private readonly System.Func<T> _createFunc;
        private readonly System.Action<T> _onGet;
        private readonly System.Action<T> _onRelease;
        private readonly System.Action<T> _onDestroy;
        private readonly int _maxSize;
        private readonly bool _collectionCheck;

        public ObjectPool(System.Func<T> createFunc, System.Action<T> onGet, System.Action<T> onRelease, System.Action<T> onDestroy, bool collectionCheck, int defaultCapacity, int maxSize)
        {
            _createFunc = createFunc;
            _onGet = onGet;
            _onRelease = onRelease;
            _onDestroy = onDestroy;
            _maxSize = maxSize;
            _collectionCheck = collectionCheck;
            _pool = new Stack<T>(defaultCapacity);
        }

        public T Get()
        {
            T element;
            if (_pool.Count == 0)
            {
                element = _createFunc();
            }
            else
            {
                element = _pool.Pop();
            }

            _onGet?.Invoke(element);
            return element;
        }

        public void Release(T element)
        {
            if (_pool.Count < _maxSize)
            {
                _onRelease?.Invoke(element);
                _pool.Push(element);
            }
            else
            {
                _onDestroy?.Invoke(element);
            }
        }

        public int CountInactive => _pool.Count;
    }
}

using System;
using System.Collections.Generic;

namespace ObjectPool
{
    public class ObjectPool<T>
    {
        private readonly Stack<T> _objects = new Stack<T>();
        private readonly Func<T> _objectGenerator;
        private readonly Action<T> _getAction;
        private readonly Action<T> _releaseAction;

        public ObjectPool(Func<T> objectGenerator, int initialCapacity, Action<T> getAction,Action<T> releaseAction)
        {
            _objectGenerator = objectGenerator;
            _getAction = getAction;
            _releaseAction = releaseAction;
            
            for (var i = 0; i < initialCapacity; ++i)
                _objects.Push(objectGenerator());
        }

        public T Get()
        {
            if (_objects.Count > 0)
            {
                var getObj = _objects.Pop();
                _getAction?.Invoke(getObj);
                return getObj;
            }

            var newObj = _objectGenerator();
            _getAction?.Invoke(newObj);
            return newObj;
        }

        public void Return(T item)
        {
            _releaseAction?.Invoke(item);
            _objects.Push(item);
        }

        public int Count => _objects.Count;
        public Type GetPoolType => typeof(T);
    }
}
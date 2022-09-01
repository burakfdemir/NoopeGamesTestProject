using System;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace Stackable
{
    public class StackableSystem : MonoBehaviour
    {
        [SerializeField] private List<StackableBase> stackablePrefabs;
        [SerializeField] private Vector3 stackableDistance = Vector3.one;
        private Stack<StackableBase> _stackables;
        private Dictionary<StackableType, ObjectPool<StackableBase>> _stackablePools;

        private int _counter;

        private void Start()
        {
            _stackables = new Stack<StackableBase>();
            CreatePools();
        }

        public void Push(StackableType stackable)
        {
            if (_stackables.Count != 0)
            {
                if (stackable != _stackables.Peek().StackableData.stackableType) return;
            }

            var newStackable = GetStackable(stackable);
            SetStackablePosition(newStackable.transform);
            _stackables.Push(newStackable);

            _counter++;
        }

        public StackableBase Pop()
        {
            if (_stackables.Count == 0) return default;

            var stackable = _stackables.Pop();
            _stackablePools[stackable.StackableData.stackableType].Return(stackable);
            _counter--;
            return stackable;
        }

        public StackableType GetCurrentStackType()
        {
            return _stackables.Count == 0 ? StackableType.None : _stackables.Peek().StackableData.stackableType;
        }

        public bool IsCurrentStackPurchasable()
        {
            if (_stackables.Count == 0) return false;
            return _stackables.Peek().StackableData.isPurchasable;
        }

        public StackableData GetCurrentStackableData()
        {
            if (_stackables.Count == 0) return default;
            return _stackables.Peek().StackableData;
        }

        private void CreatePools()
        {
            _stackablePools = new Dictionary<StackableType, ObjectPool<StackableBase>>();

            foreach (var prefab in stackablePrefabs)
            {
                var pool = new ObjectPool<StackableBase>(() =>
                    {
                        var newObj = Instantiate(prefab, transform, true)
                            .GetComponent<StackableBase>();
                        newObj.gameObject.SetActive(false);
                        return newObj;
                    },
                    5,
                    (stackable) => stackable.gameObject.SetActive(true),
                    (stackable) => stackable.gameObject.SetActive(false));

                var typeCheckObject = pool.Get();
                _stackablePools.Add(typeCheckObject.StackableData.stackableType, pool);
                pool.Return(typeCheckObject);
            }
        }

        private StackableBase GetStackable(StackableType stackable)
        {
            return _stackablePools[stackable].Get();
        }

        private void SetStackablePosition(Transform sTransform)
        {
            sTransform.position = transform.position + _counter * stackableDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1f);
        }
    }

    public enum StackableType
    {
        None,
        Wheat,
        Carrot,
        Money
    }
}
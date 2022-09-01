using System;
using System.Collections.Generic;
using System.Linq;
using ObjectPool;
using Plants;
using UnityEngine;

namespace StackableFields
{
    public class StackableFieldBase : MonoBehaviour
    {
        [SerializeField] protected Transform up;
        [SerializeField] protected Transform down;
        [SerializeField] protected Transform right;
        [SerializeField] protected Transform left;

        [SerializeField] protected PlantFieldData data;
        [SerializeField] protected GameObject prefab;
        protected ObjectPool<IField> _fieldPool;
        protected Dictionary<IField, Vector3> _fieldPosDict;

        public event Action OnAllFieldsGrow;
        public event Action OnAllFieldsPicked;

        protected virtual void Start()
        {
            _fieldPosDict = new Dictionary<IField, Vector3>(data.GetSize);
            _fieldPool = new ObjectPool<IField>(() => Instantiate(prefab,transform,false).GetComponent<IField>(),
                data.GetSize,
                null,
                null);
        }
        
        protected virtual void CalculatePlantPositions()
        {
            var xPadding = Vector3.Distance(right.transform.position ,left.transform.position) / data.xSize + 1;
            var yPadding = Vector3.Distance(up.transform.position, down.transform.position) / data.ySize + 1;

            for (var x = 0; x < data.xSize; x++)
            {
                for (var y = 0; y < data.ySize; y++)
                {
                    var pos = new Vector3(x * xPadding, 0f, y * yPadding) + new Vector3(
                        left.position.x, 0f,
                        down.position.z);
                    _fieldPosDict.Add(_fieldPool.Get(),pos);
                }
            }
        }

        protected void GrowFields()
        {
            foreach (var plant in _fieldPosDict)
            {
                plant.Key.Grow(plant.Value);
            }
        }

        protected void OnDrawGizmos()
        {
            if(up == null ||down == null ||right == null ||left == null) return;
            
            Gizmos.color = Color.cyan;
            
            var pos = (up.position + down.position + left.position + right.position)/4; 
            Gizmos.DrawSphere(pos,2f);

            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(pos,
                new Vector3(Vector3.Distance(right.position, left.position), 1f,
                    Vector3.Distance(up.position, down.position)));
            
        }

        protected void InvokeAllFieldsGrow()
        {
            OnAllFieldsGrow?.Invoke();
        }

        protected void InvokeAllFieldsPicked()
        {
            OnAllFieldsPicked?.Invoke();
        }
    }
}
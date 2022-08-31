using System;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace Plants
{
    public class PlantField : MonoBehaviour
    {
        [SerializeField] private Transform up;
        [SerializeField] private Transform down;
        [SerializeField] private Transform right;
        [SerializeField] private Transform left;

        [SerializeField] private PlantFieldData data;
        [SerializeField] private GameObject prefab;
        private ObjectPool<IPlant> _plantPool;
        private Dictionary<IPlant, Vector3> _plantPosDict;

        private void Start()
        {
            _plantPosDict = new Dictionary<IPlant, Vector3>(data.GetSize);
            _plantPool = new ObjectPool<IPlant>(() => Instantiate(prefab,transform,false).GetComponent<IPlant>(),
                data.GetSize,
                null,
                null);
            
            CalculatePlantPositions();
            GrowPlants();
        }

        private void CalculatePlantPositions()
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
                    _plantPosDict.Add(_plantPool.Get(),pos);
                }
            }
        }

        private void GrowPlants()
        {
            foreach (var plant in _plantPosDict)
            {
                plant.Key.Grow(plant.Value);
            }
        }

        private void OnDrawGizmos()
        {
            if(up == null ||down == null ||right == null ||left == null) return;
            
            Gizmos.color = Color.magenta;

            var pos = (up.position + down.position + left.position + right.position)/4; 
            Gizmos.DrawCube(pos,
                new Vector3(Vector3.Distance(right.position, left.position), 1f,
                    Vector3.Distance(up.position, down.position)));
            
        }
    }
}
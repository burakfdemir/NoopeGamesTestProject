using UnityEngine;

namespace Plants
{
    [CreateAssetMenu(menuName = "ScriptableData/PlantAreaData", fileName = "PlantAreaData", order = 0)]
    public class PlantFieldData : ScriptableObject
    {
        public int xSize;
        public int ySize;
        public int GetSize => xSize * ySize;
    }
}
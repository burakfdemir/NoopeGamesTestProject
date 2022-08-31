using UnityEngine;

namespace Plants
{
    [CreateAssetMenu(fileName = "Plant Data", menuName = "ScriptableData/PlantData", order = 0)]
    public class PlantData : ScriptableObject
    {
        public Vector2 growTimeRange = new Vector2(3f, 12f);
    }
}
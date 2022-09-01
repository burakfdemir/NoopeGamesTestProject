using System.Collections.Generic;
using Stackable;
using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(menuName = "ScriptableData/BuildingData", fileName = "Building Data", order = 0)]
    public class BuildingData : ScriptableObject
    {
        public string buildingName;
        public bool isStore;
        public bool isPurchasableBuilding;
        public float buildingPrice;
        public float productCreationTime;
        public StackableType sellingType;
        public List<StackableType> buyingTypes;
        public float stackItemPopTime = 0.1f;

    }
}
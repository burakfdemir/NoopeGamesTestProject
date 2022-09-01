using System;
using Stackable;
using Unity.VisualScripting;
using UnityEngine;

namespace Buildings
{
    public class BuildingBase : MonoBehaviour, IBuilding
    {
        [SerializeField] protected BuildingData data;
        public BuildingData Data => data;
        protected YieldInstruction wait;

        protected virtual void Start()
        {
            wait = new WaitForSeconds(data.stackItemPopTime);
        }

        protected bool ValidateSystem(Collider other, out StackableSystem system)
        {
            system = other.GetComponentInChildren<StackableSystem>();
            return system != null;
        }

    }
}
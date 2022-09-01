using System;
using Plants;
using UnityEngine;

namespace Stackable.Helpers
{
    public class StackAdder : MonoBehaviour
    {
        [SerializeField] private StackableBase stackable;
        [SerializeField] private FieldBase field;

        private void Start()
        {
            stackable = GetComponent<StackableBase>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var system = other.GetComponentInChildren<StackableSystem>();
            if(system == null) return;

            var systemType = system.GetCurrentStackType();
            if(stackable.StackableData.stackableType != system.GetCurrentStackType() && systemType != StackableType.None) return;
            
            system.Push(stackable.StackableData.stackableType);
            field.Pick();
        }
    }
}
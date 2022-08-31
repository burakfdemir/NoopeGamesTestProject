using System;
using UnityEngine;

namespace Stackable.Helpers
{
    public class StackAdder : MonoBehaviour
    {
        [SerializeField] private StackableBase stackable;

        private void Start()
        {
            stackable = GetComponent<StackableBase>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var system = other.GetComponentInChildren<StackableSystem>();
            if(system == null) return;
            
            system.Push(stackable.StackableData.stackableType);
        }
    }
}
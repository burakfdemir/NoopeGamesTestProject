using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stackable
{
    [System.Serializable]
    public class StackableBase : MonoBehaviour, IStackable
    {
        [SerializeField] private StackableData data;
        public StackableData StackableData => data;
    }
}
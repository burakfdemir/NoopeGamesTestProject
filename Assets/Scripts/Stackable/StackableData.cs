using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stackable
{
    [CreateAssetMenu(menuName = "ScriptableData/StackableData", fileName = "StackableData", order = 0)]
    public class StackableData : ScriptableObject
    {
        public StackableType stackableType;
    }
}
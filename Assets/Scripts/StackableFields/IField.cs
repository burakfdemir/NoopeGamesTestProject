using System;
using UnityEngine;

namespace Plants
{
    public interface IField
    {
        void Grow(Vector3 target);
        void Pick();
        event Action OnGrow;
        event Action OnPick;
    }
}
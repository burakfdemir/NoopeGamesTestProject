using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "ScriptableData/PlayerData", fileName = "PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float speed;
    }
}
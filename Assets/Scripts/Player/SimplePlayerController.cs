using System;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class SimplePlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData data;

        private void Update()
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");
            var speed = data.movementSpeed;

            var playerTransform = transform;
            var oldPos = playerTransform.position;
            var newPos = new Vector3(horizontal * speed * Time.deltaTime, 0f, vertical * speed * Time.deltaTime);
            playerTransform.position += newPos;
        }
    }
}
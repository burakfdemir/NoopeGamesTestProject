using System;
using UnityEngine;

namespace CameraFollow
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]private Vector3 distance = new Vector3(0f,7f,30f);
        private Transform _target;
        private void Start()
        {
            _target = GameObject.FindWithTag("CameraFollowTarget").GetComponent<Transform>();
        }

        private void Update()
        {
            transform.position = _target.position + distance;
            transform.LookAt(_target);
        }
    }
}
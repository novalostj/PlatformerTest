using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class GroundChecker : MonoBehaviour
    {
        public float distance;
        public LayerMask targetLayer;

        public bool IsGrounded { get; set; } = false;
        
        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, targetLayer);
            IsGrounded = hit.collider;
        }
        
    }
}

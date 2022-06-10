using System;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Range(0, 1)] public float belowDistance = 0.5f;
        
        
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            IDamagable dest = col.collider.GetComponent<IDamagable>();
            if (dest == null) return;
            
            Vector3 targetPos = col.transform.position;
            
            if (targetPos.y < transform.position.y - belowDistance)
            {
                dest.Damaged();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Range(0, 0.5f)]
    public float belowDistance = 0.5f;
    
    
    private Transform target;
    
    private void Update()
    {
        if (target)
        {
            IDamagable dest = target.GetComponent<IDamagable>();
            
            if (transform.position.y >= target.position.y - belowDistance)
            {
                dest.Damaged();
            }

            target = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player")) target = col.transform;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        IDamagable dest = col.collider.GetComponent<IDamagable>();

        if (dest != null)
        {
            dest.Damaged();
        }
    }
}

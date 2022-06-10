using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Monsters;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public EnemyMovementAI movementAI;

    public float sightDistance = 5f;
    public float distanceToTurn = 1.5f;
    public LayerMask targetLayer;
    public float checkInterval = 0.1f;

    public bool TargetInSight { get; private set; }

    public bool ShouldTurn { get; set; }


    private void Start()
    {
        StartCoroutine(CheckIfTargetInSight()); 
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, movementAI.MoveDir * sightDistance, Color.red);
    }

    private IEnumerator CheckIfTargetInSight()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            Vector2 position = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(position, movementAI.MoveDir, sightDistance, targetLayer);
            
            
            if (hit.collider)
            {
                TargetInSight = hit.transform.CompareTag("Player");
                
                ShouldTurn = hit.transform.CompareTag("Ground") && Vector2.Distance(hit.point, transform.position) < distanceToTurn;
            }
            else if (!TargetInSight)
            {
                RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(position.x + 0.5f, position.y), (movementAI.MoveDir + Vector2.down).normalized, distanceToTurn, targetLayer);

                ShouldTurn = !hit2.collider;
            }
        }
    }
}

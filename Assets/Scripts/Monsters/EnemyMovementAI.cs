using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Monsters
{
    public class EnemyMovementAI : MonoBehaviour, IDamagable
    {
        public EnemySight sight;
    
        private Rigidbody2D rb;
        
        public bool canMove = true;
        public float speed;

        public Vector2 MoveDir { get; private set; } = Vector2.left;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            if (sight.ShouldTurn)
            {
                sight.ShouldTurn = false;
                ToggleDir();
            }
        }

        private void FixedUpdate()
        {
            Vector2 movement = canMove ? 
                new Vector2(MoveDir.x * (speed * 10 * Time.fixedDeltaTime), rb.velocity.y)  : new Vector2(0, rb.velocity.y);

            rb.velocity = sight.TargetInSight ? new Vector2(movement.x * 2, movement.y) : movement;
        }

        private void ToggleDir()
        {
            MoveDir = MoveDir == Vector2.right ? Vector2.left : Vector2.right;
        }

        public void Damaged()
        {
            Destroy(gameObject);
        }
    }
}

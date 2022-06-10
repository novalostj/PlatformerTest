﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [System.Serializable]
    public class JumpSettings
    {
        public float jumpStrength = 20f;
        public bool canDoubleJump = false;

        public float maxJumpStrenth;
        public GroundChecker floorDetection;

        public bool doubleJumpToggler = false;
    }
    
    public class Movement : MonoBehaviour, IDamagable
    {
        private int timesJumpd = 0;
        public delegate void Player(Transform transform);
        public static Player pDead;

        public delegate void PlayerSound();
        public static PlayerSound pJump;
        public static PlayerSound pHit;
        
        public float speed = 2f;
        public AnimationCurve speedCurving;
        public JumpSettings jumpSettings;
        
        private Rigidbody2D rb;

        private float forwardHoldTime;

        public bool canMove = true;

        public Vector2 MovementDirection { get; private set; } = new Vector2();

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!canMove)
            {
                rb.velocity = new Vector2();
                return;
            }
            
            if ((!jumpSettings.floorDetection.IsGrounded && !jumpSettings.canDoubleJump && rb.velocity.y < 0 && jumpSettings.doubleJumpToggler) || 
                (rb.velocity.y < 0 && timesJumpd == 0) ||
                (timesJumpd == 1 && !jumpSettings.floorDetection.IsGrounded && MovementDirection.y == 0))
            {
                jumpSettings.canDoubleJump = true;
                jumpSettings.doubleJumpToggler = false;
            }

            if (jumpSettings.floorDetection.IsGrounded)
            {
                jumpSettings.canDoubleJump = false;
                timesJumpd = 0;
            }

            forwardHoldTime = MovementDirection.x > 0 || MovementDirection.x < 0 ? Mathf.Clamp(forwardHoldTime + Time.deltaTime, 0, 1) : Mathf.Clamp(forwardHoldTime - Time.deltaTime, 0, 1);
        }

        private void FixedUpdate()
        {
            if (!canMove) return;
            
            Walk();
            Jump();
            VelocityLimiter();
        }

        private void Walk()
        {
            float horizontal = MovementDirection.x * 10 * (speed * Time.fixedDeltaTime) * speedCurving.Evaluate(forwardHoldTime);

            rb.velocity = new Vector2(horizontal, rb.velocity.y);
        }

        private void Jump()
        {
            if (MovementDirection.y > 0)
            {
                if (jumpSettings.floorDetection.IsGrounded && !jumpSettings.doubleJumpToggler && rb.velocity.y < 15f)
                {
                    pJump?.Invoke();
                    timesJumpd += 1;
                    rb.velocity = new Vector2(rb.velocity.x, jumpSettings.jumpStrength);
                    jumpSettings.doubleJumpToggler = true;
                    jumpSettings.floorDetection.IsGrounded = false;
                }
                else if (jumpSettings.canDoubleJump)
                {
                    pJump?.Invoke();
                    timesJumpd += 1;
                    jumpSettings.canDoubleJump = false;
                    rb.velocity = new Vector2(rb.velocity.x, jumpSettings.jumpStrength);
                }
            }
        }

        private void VelocityLimiter()
        {
            Vector2 vel = new Vector2(rb.velocity.x,
                Mathf.Clamp(rb.velocity.y, -jumpSettings.maxJumpStrenth, jumpSettings.maxJumpStrenth));
            rb.velocity = vel;
        }

        public void Damaged()
        {
            pHit?.Invoke();
            pDead?.Invoke(transform);
        }

        public void SetHorizontalVelocity(float i)
        {
            MovementDirection = new Vector2(i, MovementDirection.y);
        }
        
        public void SetVerticalVelocity(float i)
        {
            MovementDirection = new Vector2(MovementDirection.x, i);
        }
    }
}

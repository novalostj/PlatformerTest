using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnim : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb;
    public EnemySight sight;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isGoingRight = rb.velocity.x > 0;

        anim.speed = sight.TargetInSight ? 1.5f : 1;
        
        anim.SetFloat("speed", rb.velocity.magnitude);
        spriteRender.flipX = isGoingRight;
    }
}

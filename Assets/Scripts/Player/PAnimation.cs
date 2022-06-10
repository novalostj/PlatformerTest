using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(Movement))]
    public class PAnimation : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Animator anim;
        private Rigidbody2D rb;
        private Movement mv;
        public GroundChecker groundChecker;
    
    
        private void Start()
        {
            mv = GetComponent<Movement>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (mv.MovementDirection.magnitude > 0) spriteRenderer.flipX = mv.MovementDirection.x < 0;

            Vector2 horizSpeed = new Vector2(rb.velocity.x, 0);
            
            anim.SetFloat("speed", horizSpeed.magnitude);
            anim.SetFloat("onAir", rb.velocity.y);
            anim.SetBool("isGrounded", groundChecker.IsGrounded);
        }
    }
}


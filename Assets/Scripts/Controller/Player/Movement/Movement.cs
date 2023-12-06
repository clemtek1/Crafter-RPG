using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Clemtek.Controller.Player.Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float sprintModifier = 1.5f;
        [SerializeField] float iTime = 3f;

        public bool IsSprinting { get; private set; }
        public bool IsInvincible { get; private set; }

        public bool CanMove { get; private set; }
        public bool IsMoving => !Mathf.Approximately(0, direction.magnitude);
        private Vector2 direction;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rb;
        private float iClock = 0f;

        // Start is called before the first frame update
        void Start()
        {
            direction = Vector2.zero;
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            IsSprinting = false;
            IsInvincible = false;
            CanMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (CanMove)
            {
                rb.velocity = (IsSprinting ? sprintModifier : 1) * speed * direction;
                animator.SetBool("Move", IsMoving);

                // Flip on non-zero movement.
                if (direction.x < 0)
                    spriteRenderer.flipX = true;
                else if (direction.x > 0)
                    spriteRenderer.flipX = false;
            }

            if (IsInvincible)
            {
                spriteRenderer.enabled = (int)((iClock - (int)iClock) * 10) % 2 == 0;
                iClock += Time.deltaTime;
                if(iClock >= iTime)
                {
                    iClock = 0f;
                    IsInvincible = false;
                    spriteRenderer.enabled = true;
                }
            }
        }

        public void Vertical(InputAction.CallbackContext context)
        {
            float yDir = context.ReadValue<float>();
            direction.y = yDir;
        }

        public void Horizontal(InputAction.CallbackContext context)
        {
            float xDir = context.ReadValue<float>();
            direction.x = xDir;
        }

        public void Recoil(Vector2 forceVector)
        {
            StartCoroutine(TakeDamage(forceVector));
        }

        public void Sprint(InputAction.CallbackContext context)
        {
            if(context.started) IsSprinting = true;
            if(context.canceled) IsSprinting = false;
        }

        private IEnumerator TakeDamage(Vector2 forceVector)
        {
            yield return null;
            CanMove = false;
            rb.velocity = Vector2.zero;
            IsInvincible = true;
            animator.SetTrigger("Hurt");

            yield return new WaitForSeconds(.05f);
            rb.AddForce(forceVector);

            yield return new WaitForSeconds(.5f);
            rb.velocity = Vector2.zero;
            CanMove = true;
        }
    }
}

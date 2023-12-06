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

        private bool isSprinting = false;
        private Vector2 direction;
        private CharacterController controller;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            direction = Vector2.zero;
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            controller.Move((isSprinting ? sprintModifier : 1) * speed * Time.deltaTime * direction);
            animator.SetBool("Move", !Mathf.Approximately(0, direction.magnitude));

            // Flip on non-zero movement.
            if (direction.x < 0)
                spriteRenderer.flipX = true;
            else if(direction.x > 0)
                spriteRenderer.flipX = false;
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

        public void Sprint(InputAction.CallbackContext context)
        {
            if(context.started) isSprinting = true;
            if(context.canceled) isSprinting = false;
        }
    }
}

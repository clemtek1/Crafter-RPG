using Clemtek.Controller.Player.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Clemtek.Controller.Player.Attack
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] int attackStamina = 2;
        [SerializeField] int flyKickStamina = 5;
        [SerializeField] int strikeStamina = 7;
        [SerializeField] float attackCooldown = .5f;

        private Movement.Movement movement;
        private Animator animator;
        private Stamina stamina;

        private bool canAttack = true;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            movement = GetComponent<Movement.Movement>();
            stamina = GetComponent<Stamina>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BaseAttack(InputAction.CallbackContext context)
        {
            if (canAttack && context.performed && stamina.LoseAttribute(attackStamina))
            {
                animator.SetTrigger("Attack");
                canAttack = false;
            }
        }

        public void FlyKick(InputAction.CallbackContext context)
        {
            if (canAttack && context.performed && stamina.LoseAttribute(flyKickStamina))
            {
                animator.SetTrigger("FlyKick");
                movement.CanMove = false;
                canAttack = false;
            }
        }

        public void Strike(InputAction.CallbackContext context)
        {
            if(canAttack && context.performed && stamina.LoseAttribute(strikeStamina))
            {
                animator.SetTrigger("Strike");
                movement.CanMove = false;
                canAttack = false;
            }
        }

        public void EndAttack()
        {
            movement.CanMove = true;
            canAttack = true;
        }
    }
}

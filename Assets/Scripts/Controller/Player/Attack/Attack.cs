using UnityEngine;
using UnityEngine.InputSystem;

namespace Clemtek.Controller.Player.Attack
{
    public class Attack : MonoBehaviour
    {

        private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BaseAttack(InputAction.CallbackContext context)
        {
            if (context.performed) animator.SetTrigger("Attack");
        }
    }
}

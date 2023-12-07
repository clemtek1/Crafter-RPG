using UnityEngine;

namespace Clemtek.Controller.Player.Attributes
{
    public class Stamina : Attribute
    {
        [SerializeField] float sprintLossRate = 1f;

        private Movement.Movement movement;

        protected override void Start()
        {
            base.Start();
            movement = GetComponent<Movement.Movement>();
            canRegen = true;
        }

        protected override void Update()
        {
            base.Update();
            if (movement.IsSprinting && movement.IsMoving)
            {
                LoseAttribute(sprintLossRate * Time.deltaTime);
                canRegen = false;
            } else
            {
                canRegen = true;
            }
        }

        new public bool LoseAttribute(float value)
        {
            if (this.value > value)
            {
                base.LoseAttribute(value);
                return true;
            }
            return false;
        }
    }
}

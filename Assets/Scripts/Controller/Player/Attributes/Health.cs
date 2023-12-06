using UnityEngine;

namespace Clemtek.Controller.Player.Attributes
{
    public class Health : Attribute
    {
        [SerializeField] float timeToRegen;

        private float regenClock;
        protected override void Start()
        {
            base.Start();
            canRegen = true;
            regenClock = timeToRegen;
        }

        protected override void Update()
        {
            base.Update();

            // Handle Regen clock
            if (regenClock > timeToRegen) canRegen = true;
            else regenClock += Time.deltaTime;
        }

        public override void LoseAttribute(float value)
        {
            base.LoseAttribute(value);
            canRegen = false;
            regenClock = 0f;
        }
    }
}

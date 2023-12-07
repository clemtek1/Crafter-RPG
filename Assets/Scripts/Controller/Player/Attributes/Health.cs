using UnityEngine;

namespace Clemtek.Controller.Player.Attributes
{
    public class Health : Attribute
    {
        [SerializeField] float timeToRegen;
        [SerializeField] float iTime = 3f;

        public bool IsInvincible { get; private set; }
        public bool IsDead => Mathf.Approximately(value, 0);

        private float regenClock;
        private float iClock = 0f;
        private SpriteRenderer spriteRenderer;

        protected override void Start()
        {
            base.Start();
            canRegen = true;
            regenClock = timeToRegen;
            IsInvincible = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Update()
        {
            base.Update();

            // Handle Regen clock
            if (regenClock > timeToRegen) canRegen = !IsDead;
            else regenClock += Time.deltaTime;

            if (IsInvincible)
            {
                spriteRenderer.enabled = (int)((iClock - (int)iClock) * 10) % 2 == 0;
                iClock += Time.deltaTime;
                if (iClock >= iTime)
                {
                    iClock = 0f;
                    IsInvincible = false;
                    spriteRenderer.enabled = true;
                }
            }
        }

        public override void LoseAttribute(float value)
        {
            base.LoseAttribute(value);
            IsInvincible = true;
            canRegen = false;
            regenClock = 0f;
            if(IsDead)
            {
                OnDeath();
            }
        }

        public void OnDeath()
        {
            GetComponent<Animator>().SetTrigger("IsDead");
            IsInvincible = false;
            canRegen = false;
        }

        public void EndDeath()
        {
            spriteRenderer.enabled = false;
        }
    }
}

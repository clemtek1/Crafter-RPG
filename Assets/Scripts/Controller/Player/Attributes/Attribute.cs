using System;
using UnityEngine;
using UnityEngine.UI;

namespace Clemtek.Controller.Player.Attributes
{
    public class Attribute : MonoBehaviour
    {
        [SerializeField] Slider uiBar;
        [SerializeField] int maxValue;
        [SerializeField] float regenValue;

        protected float value;
        protected bool canRegen = false;

        protected virtual void Start()
        {
            value = maxValue;
            uiBar.maxValue = maxValue;
            uiBar.value = value;
        }

        protected virtual void Update()
        {
            if (canRegen) GainAttribute(regenValue * Time.deltaTime);
        }

        public void GainAttribute(float value)
        {
            this.value = Math.Min(maxValue, this.value + value);
            UpdateUIBar();
        }

        public virtual void LoseAttribute(float value)
        {
            this.value = Math.Max(0, this.value - value);
            UpdateUIBar();
        }

        private void UpdateUIBar()
        {
            uiBar.value = value;
        }
    }
}

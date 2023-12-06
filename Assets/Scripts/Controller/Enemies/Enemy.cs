using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clemtek.Controller.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public int DamageDealt { get; private set; }
        public float RecoilForce { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            DamageDealt = 5;
            RecoilForce = 600;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

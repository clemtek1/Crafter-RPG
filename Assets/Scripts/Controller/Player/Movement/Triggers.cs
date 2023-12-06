using Clemtek.Controller.Enemies;
using Clemtek.Controller.Player.Attributes;
using UnityEngine;

namespace Clemtek.Controller.Player.Movement
{
    [RequireComponent(typeof(Movement))]
    public class Triggers : MonoBehaviour
    {

        Movement movement;

        void Start()
        {
            movement = GetComponent<Movement>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch(collision.tag)
            {
                case "Enemy":
                    if(!movement.IsInvincible)
                        TakeDamage(collision.gameObject.GetComponent<Enemy>());
                    break;
            }
        }

        private void TakeDamage(Enemy enemy)
        {
            GetComponent<Health>().LoseAttribute(enemy.DamageDealt);
            Vector2 backwardDirection = transform.position - enemy.transform.position;
            movement.Recoil(enemy.RecoilForce * backwardDirection);
        }


    }
}

using Clemtek.Controller.Enemies;
using Clemtek.Controller.Player.Attributes;
using UnityEngine;

namespace Clemtek.Controller.Player.Movement
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Movement))]
    public class Triggers : MonoBehaviour
    {

        Health health;
        Movement movement;

        void Start()
        {
            health = GetComponent<Health>();
            movement = GetComponent<Movement>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            switch(collision.tag)
            {
                case "Enemy":
                    if(!health.IsInvincible && !health.IsDead)
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

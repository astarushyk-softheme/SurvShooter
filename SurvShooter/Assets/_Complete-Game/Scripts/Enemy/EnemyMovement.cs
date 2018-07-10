using UnityEngine;
using System.Collections;
using System.Linq;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
        EnemyHealth enemyHealth;                        // Reference to this enemy's health.
        UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.


        void Awake ()
        {
            // Set up the references.
            enemyHealth = GetComponent<EnemyHealth> ();
            nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        }


        void Update ()
        {
            var closestPlayer = FindClosestPlayer();

            if(closestPlayer == null)
                nav.enabled = false;
            else
                nav.enabled = true;
            
            var playerHealth = closestPlayer.GetComponent<PlayerHealth>();            

            if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                nav.SetDestination (closestPlayer.transform.position);
            }
            else
            {
                nav.enabled = false;
            }
        }

        GameObject FindClosestPlayer()
        {
            var players = GameObject.FindGameObjectsWithTag("Player");

            if(players.Count() == 0)
            {
                return null;
            }

            float minDistance = Mathf.Infinity;

            GameObject closestPlayer = null;

            foreach(var player in players)
            {
                var distance = Vector3.Distance(player.transform.position, transform.position);

                if(distance < minDistance)
                {
                    minDistance = distance;
                    closestPlayer = player;
                }
            }

            return closestPlayer;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float wayPointDewlltime = 1f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;

        Vector3 guardPosition;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;

        int currentWayPointIndex = 0;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;

        }


        void Update()
        {
            if(health.IsDead())
                return;

            if(inAttackRangeOfPlayer()  && fighter.canAttack(player))
            {
                attackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                suspicionBehaviour();
            }
            
            else
            {
                patrolBehaviour();
            }


            updateTimers();

        }

        void updateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        void patrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if(patrolPath != null)
            {
                if(atWayPoint())
                {
                    timeSinceArrivedAtWaypoint = 0f;
                    cycleWayPoint();
                }

                nextPosition = getCurrentWayPoint();
            }

            if(timeSinceArrivedAtWaypoint > wayPointDewlltime)
            {
                mover.startMoveAction(nextPosition, patrolSpeedFraction);
            }

        }

        bool atWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, getCurrentWayPoint());
            return distanceToWaypoint <= wayPointTolerance;
        }

        void cycleWayPoint()
        {
            currentWayPointIndex = patrolPath.getNextIndex(currentWayPointIndex);
        }

        Vector3 getCurrentWayPoint()
        {
            return patrolPath.getWayPoint(currentWayPointIndex);
        }

        void suspicionBehaviour()
        {
            GetComponent<ActionScheduler>().cancelCurrentAction();
        }

        void attackBehaviour()
        {
            timeSinceLastSawPlayer = 0f;
            fighter.attack(player);
        }

        bool inAttackRangeOfPlayer()
        {
            
            float distanceToPlayer =  Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        
        // Called by Unity

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }


    }
}

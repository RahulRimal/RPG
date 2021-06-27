using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        NavMeshAgent NavMeshAgent;
        Health health;

        [SerializeField] float maxSpeed = 6f;

        void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            NavMeshAgent.enabled = !health.IsDead();

            updateAnimator();
        }
        
        public void startMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().startAction(this);
            moveTo(destination, speedFraction);
        }

        public void moveTo(Vector3 destination, float speedFraction)
        {
            NavMeshAgent.destination = destination;
            NavMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            NavMeshAgent.isStopped = false;
        }

        public void cancel()
        {
            NavMeshAgent.isStopped = true;
        }

        void updateAnimator()
        {
            Vector3 velocity = NavMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }



    }
    
}
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

        void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            updateAnimator();
        }
        
        public void startMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().startAction(this);
            moveTo(destination);
        }

        public void moveTo(Vector3 destination)
        {
            NavMeshAgent.destination = destination;
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
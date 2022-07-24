using Omniworlds.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Omniworlds.Scripts.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private Transform _target;
        private Ray _lastRay;

        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            //Grab global and convert to local velocity to aid the animator for speed
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            //Set the speed as the forward local velocity
            float speed = localVelocity.z;

            //Update the speed in the animator
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}
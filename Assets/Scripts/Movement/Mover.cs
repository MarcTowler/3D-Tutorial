using UnityEngine;
using UnityEngine.AI;

namespace Omniworlds.Scripts.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private Ray _lastRay;


        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            //Grab global and convert to local velocity to aid the animator for speed
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            //Set the speed as the forward local velocity
            float speed = localVelocity.z;

            //Update the speed in the animator
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
        }
    }
}

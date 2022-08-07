using Omniworlds.Scripts.Movement;
using Omniworlds.Scripts.Combat;
using Omniworlds.Scripts.Core;
using UnityEngine;

namespace Omniworlds.Scripts.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private float _chaseDistance = 5f;
        
        [SerializeField]
        private float _suspiciousTime = 5f;
        
        [SerializeField] 
        private PatrolPath _patrolPath;
        
        [SerializeField]
        private float _waypointTolerance = 1f;
        
        [SerializeField]
        private float _waypointDwellTime = 2f;
        
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private GameObject _player;

        private Vector3 _guardPosition;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex = 0;
        
        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _player = GameObject.FindWithTag("Player");
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _guardPosition = transform.position;
        }
        private void Update()
        {
            if(_health.IsDead)
            {
                return;
            }
            
            if(InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                AttackBehaviour();
            }
            else if (_timeSinceLastSawPlayer < _suspiciousTime)
            {
                //Suspicion
                _timeSinceLastSawPlayer += Time.deltaTime;
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
            _timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _guardPosition;
            
            if(_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                
                nextPosition = GetCurrentWaypoint();
            }
            
            if(_timeSinceArrivedAtWaypoint > _waypointDwellTime)
            {
                _mover.StartMoveAction(nextPosition);
                
            }
        }
        
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            
            return distanceToWaypoint < _waypointTolerance;
        }
        
        private void CycleWaypoint()
        {
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);
        }
        
        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
        }

        private void ChasePlayer()
        {
            _fighter.Attack(_player);
            print(gameObject.name + " should chase");
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            
            return distanceToPlayer < _chaseDistance;
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}
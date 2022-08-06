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
        
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private GameObject _player;

        private Vector3 _guardPosition;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        
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
                _timeSinceLastSawPlayer = 0;
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
                GuardBehaviour();
            }
            
            _timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            _mover.StartMoveAction(_guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
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
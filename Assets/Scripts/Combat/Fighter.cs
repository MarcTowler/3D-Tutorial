using Omniworlds.Scripts.Core;
using Omniworlds.Scripts.Movement;
using UnityEngine;

namespace Omniworlds.Scripts.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] 
        private float _weaponRange = 2f;
        
        [SerializeField]
        private float _timeBetweenAttacks = 1.21f;
        
        [SerializeField]
        private float _weaponDamage = 10f;
        
        private Health _target;
        private float _timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target == null) return;
            if(_target.IsDead) return;
            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(_target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            //Turn to look at the enemy we have targeted
            transform.LookAt(_target.transform);
            
            if(_timeSinceLastAttack > _timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                TriggerAttack();
                _timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;
        }
        
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            
            Health targetToTest = combatTarget.GetComponent<Health>();
            
            return targetToTest != null && !targetToTest.IsDead;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            
            _target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            _target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        //Animation event
        private void Hit()
        {
            if(_target == null) return;
            _target.TakeDamage(_weaponDamage);
        }
}
}
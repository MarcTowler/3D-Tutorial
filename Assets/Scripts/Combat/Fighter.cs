using Omniworlds.Scripts.Core;
using Omniworlds.Scripts.Movement;
using UnityEngine;

namespace Omniworlds.Scripts.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] 
        private float _weaponRange = 2f;
        
        private Transform _target;

        private void Update()
        {
            if (_target == null) return;
            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(_target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }
    }
}
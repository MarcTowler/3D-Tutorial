using UnityEngine;
using UnityEngine.Serialization;

namespace Omniworlds.Scripts.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] 
        private int _maxHealth = 100;
        
        [SerializeField]
        private float _healthPoints = 100f;

        private bool _isDead = false;
        
        public bool IsDead => _isDead;
        
        public void TakeDamage(float damage)
        {
            _healthPoints = Mathf.Clamp(_healthPoints - damage, 0, _maxHealth);

            if (_healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if(_isDead) return;
            _isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
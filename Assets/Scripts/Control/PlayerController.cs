﻿using Omniworlds.Scripts.Combat;
using Omniworlds.Scripts.Core;
using Omniworlds.Scripts.Movement;
using UnityEngine;

namespace Omniworlds.Scripts.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health _health;
        
        private void Start()
        {
            _health = GetComponent<Health>();
        }
        
        private void Update()
        {
            if(_health.IsDead)
            {
                return;
            }
            
            if (InteractWithCombat())
            {
                return;
            }

            if (InteractWithMovement())
            {
                return;
            }
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                if(target == null)
                {
                    continue;
                }

                
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }

                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
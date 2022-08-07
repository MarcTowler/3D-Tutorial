﻿using System;
using UnityEngine;

namespace Omniworlds.Scripts.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float WaypointGizmoRadius = 0.3f;
        private void OnDrawGizmos()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                
                Gizmos.DrawSphere(GetWaypoint(i), WaypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if(i == transform.childCount - 1)
            {
                return 0;
            }
            
            return i + 1;
        }
        
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
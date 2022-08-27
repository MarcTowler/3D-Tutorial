using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Omniworlds.Scripts.Cinematics
{
    public class CinematicsTrigger : MonoBehaviour
    {
        private bool _triggered = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_triggered && other.gameObject.tag == "Player")
            {
                _triggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
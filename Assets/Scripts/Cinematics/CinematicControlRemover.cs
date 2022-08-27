using Omniworlds.Scripts.Control;
using Omniworlds.Scripts.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace Omniworlds.Scripts.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject _player = GameObject.FindWithTag("Player");
        
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void DisableControl(PlayableDirector pd)
        {
            
            _player.GetComponent<ActionScheduler>().CancelCurrentAction();
            _player.GetComponent<PlayerController>().enabled = false;
        }
        
        private void EnableControl(PlayableDirector pd)
        {
            _player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
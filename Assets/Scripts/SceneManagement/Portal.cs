using UnityEngine;
using UnityEngine.SceneManagement;

namespace Omniworlds.Scripts.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
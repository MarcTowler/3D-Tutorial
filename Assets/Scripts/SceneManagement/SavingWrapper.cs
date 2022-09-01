using Omniworlds.Scripts.Saving;
using UnityEngine;

namespace Omniworlds.Scripts.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string DefaultSaveFile = "save";
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }
        
        private void Load()
        {
            GetComponent<SavingSystem>().Load(DefaultSaveFile);
        }
        
        private void Save()
        {
            GetComponent<SavingSystem>().Save(DefaultSaveFile);
        }
    }
}
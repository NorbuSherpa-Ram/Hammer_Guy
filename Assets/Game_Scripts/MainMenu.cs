using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        public UnityEvent OnStart;

        public void OnStartClick()
        {
            OnStart?.Invoke();
        }



        public void LoadLevel(int _index)
        {
            SceneManager.LoadScene(_index);
        }
    }
}


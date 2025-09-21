using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StopMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject stopMenu;
        public bool IsPaused { get; private set; }
        
        private void Awake()
        {
            stopMenu.SetActive(false);
            IsPaused = false;
        }
        
        public void Resume()
        {
            Time.timeScale = 1;
            IsPaused = false;
            stopMenu.SetActive(false);
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
        
        public void StopMenu()
        {
            Time.timeScale = 0;
            IsPaused = true;
            stopMenu.SetActive(true);   
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}
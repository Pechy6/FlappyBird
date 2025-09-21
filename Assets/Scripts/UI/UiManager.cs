using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject scoreManager;
        public bool IsGameOver { get; private set; }

        private void Awake()
        {
            if (scoreManager == null)
            {
                Debug.LogWarning("ScoreManager not found in SerializeField");
            }

            gameOverPanel.SetActive(false);
            IsGameOver = false;
        }

        public void UiOnDeath()
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            scoreManager.SetActive(false);
        }

        public void PlayAgain()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
            scoreManager.SetActive(true);
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
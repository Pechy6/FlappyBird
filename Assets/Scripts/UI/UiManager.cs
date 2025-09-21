using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        private void Awake()
        {
            gameOverPanel.SetActive(false);
        }
        public static void UiOnDeath()
        {
            Time.timeScale = 0;
        }
    }
}
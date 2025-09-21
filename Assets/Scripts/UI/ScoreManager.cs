using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class ScoreManager : MonoBehaviour
    {
        public static int Score;
        [SerializeField] private TMP_Text scoreText;
        
        private void Awake()
        {
            scoreText = GetComponentInChildren<TMP_Text>();
            if (scoreText == null)
                Debug.LogError("TMP_Text not found.");
            Score = 0;
        }
        
        private void Update()
        {
            scoreText.text = $"Score: {Score}";
        }
    }
}

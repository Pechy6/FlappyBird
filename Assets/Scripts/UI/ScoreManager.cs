using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreManager : MonoBehaviour
    {
        public static int Score;
        private TMP_Text _scoreText;
        
        private void Awake()
        {
            _scoreText = GetComponent<TMP_Text>();
            Score = 0;
        }
        
        private void Update()
        {
            _scoreText.text = $"Score: {Score}";
        }
    }
}

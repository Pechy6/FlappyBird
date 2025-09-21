using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static SpriteRenderer SpriteRenderer;
        
        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            if (SpriteRenderer == null)
                Debug.LogError("SpriteRenderer not found.");
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("AddScore"))
            {
                ScoreManager.Score++;
            }
        }
    }
}
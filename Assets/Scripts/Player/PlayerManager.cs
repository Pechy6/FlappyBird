using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        // Death timer
        [SerializeField] private float deathTimer = 1f;

        private SpriteRenderer _spriteRenderer;

        // Player Collision
        private PlayerCollision _playerCollision;

        // Player Movement
        private PlayerMovement _playerMovement;

        private bool _addingScore;

        [SerializeField] private UiManager uiManager;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
                Debug.LogError("SpriteRenderer not found.");
            _playerCollision = GetComponent<PlayerCollision>();
            if (_playerCollision == null)
                Debug.LogError("PlayerCollision not found.");
            _playerMovement = GetComponent<PlayerMovement>();
            if (_playerMovement == null)
                Debug.LogError("BirdMovement not found.");
            _addingScore = true;
        }

        private void Start()
        {
            if (uiManager == null)
            {
                Debug.LogWarning("UiManager not found. in serialzfield");
                uiManager = FindAnyObjectByType<UiManager>();
                if (uiManager == null)
                    Debug.LogError("UiManager not found.");
            }
        }

        public void OnDeath()
        {
            _playerCollision.SetCrashed(true); // Set player as crashed
            _playerMovement.SetJump(); // Disable jump
            _spriteRenderer.color = Color.red; // Set sprite color to red
            _spriteRenderer.flipY = true; // Flip opposite direction
            _addingScore = false; // Disable adding score
            StartCoroutine(DelayedUiOnDeath()); // Delayed Ui On Death
        }
        
        private IEnumerator DelayedUiOnDeath()
        {
            yield return new WaitForSeconds(deathTimer);
            uiManager.UiOnDeath();
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("AddScore") && _addingScore)
            {
                ScoreManager.Score++;
            }
        }
    }
}
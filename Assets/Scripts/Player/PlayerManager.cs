using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
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

        // For menu control
        private bool _isCrashed;

        [SerializeField] private UiManager uiManager;
        [SerializeField] private StopMenuManager stopMenuManager;

        // escape menu
        private InputAction _escapeInput;
        
        //Audio
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip hitCLip;
        [SerializeField] private AudioClip deathClip;
        [SerializeField] private AudioClip scoreClip;
        private bool _isPlayed;

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
            _escapeInput = new InputAction("Escape", binding: "<Keyboard>/escape");
            _isCrashed = false;
            _isPlayed = false;
        }

        private void Start()
        {
            if (uiManager == null)
            {
                Debug.LogWarning("UiManager not found. in serialize field");
                uiManager = FindAnyObjectByType<UiManager>();
                if (uiManager == null)
                    Debug.LogError("UiManager not found.");
            }

            if (stopMenuManager == null)
            {
                Debug.LogWarning("StopMenuManager not found. in serialize field");
                stopMenuManager = FindAnyObjectByType<StopMenuManager>();
                if (stopMenuManager == null)
                    Debug.LogError("StopMenuManager not found.");
            }
        }

        private void OnEnable()
        {
            _escapeInput.Enable();
            _escapeInput.performed += OnEscape;
        }

        private void OnDisable()
        {
            _escapeInput.performed -= OnEscape;
            _escapeInput.Disable();
        }

        public void OnDeath()
        {
            _playerCollision.SetCrashed(true); // Set player as crashed
            _playerMovement.SetJump(); // Disable jump
            _spriteRenderer.color = Color.red; // Set sprite color to red
            _spriteRenderer.flipY = true; // Flip opposite direction
            _addingScore = false; // Disable adding score
            StartCoroutine(DelayedUiOnDeath()); // Delayed Ui On Death
            _isCrashed = true;
            if (audioSource != null && hitCLip != null && !_isPlayed)
            {
                audioSource.PlayOneShot(hitCLip);
                Invoke(nameof(OnDeathSound), 0.2f);
                _isPlayed = true;
            }
        }

        private void OnDeathSound()
        {
            audioSource.PlayOneShot(deathClip);
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
                if (audioSource != null && scoreClip != null)
                    audioSource.PlayOneShot(scoreClip);
            }
        }

        private void OnEscape(InputAction.CallbackContext ctx)
        {
            if (_isCrashed) return;
            // zajisti referenci po restartu scény (lazy lookup)
            if (stopMenuManager == null || stopMenuManager.gameObject == null)
            {
#if UNITY_2023_1_OR_NEWER
                stopMenuManager = FindAnyObjectByType<StopMenuManager>(FindObjectsInactive.Include);
#else
                stopMenuManager = FindObjectOfType<StopMenuManager>(true);
#endif
                if (stopMenuManager == null) return;
            }

            if (stopMenuManager.IsPaused)
                stopMenuManager.Resume();
            else
                stopMenuManager.StopMenu();
        }
    }
}
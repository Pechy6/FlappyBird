using System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;

        // Jump 
        private InputAction _jumpInput;
        [SerializeField] private float jumpForce;
        
        // Rotation
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float maxUpAngle = 35f;
        [SerializeField] private float maxDownAngle = -40f;
        

        // Manager
        private PlayerManager _playerManager;
        [SerializeField] private UiManager uiManager;
        [SerializeField] private StopMenuManager stopMenuManager;

        // Animator
        private static readonly int OnFly = Animator.StringToHash("OnFly");
        private Animator _animator;

        // Audio
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerManager = GetComponent<PlayerManager>();
            _animator = GetComponent<Animator>();
            if (_playerManager == null)
            {
                Debug.LogWarning("PlayerCollision not found.");
            }

            _jumpInput = new InputAction("Jump", binding: "<Keyboard>/space");
        }

        private void Start()
        {
            if (uiManager == null)
            {
                uiManager = FindAnyObjectByType<UiManager>();
                if (uiManager == null)
                    Debug.LogError("UiManager not found.");
            }

            if (stopMenuManager == null)
            {
                stopMenuManager = FindAnyObjectByType<StopMenuManager>();
                if (stopMenuManager == null)
                    Debug.LogError("StopMenuManager not found.");
            }

            _jumpInput.performed += ctx =>
            {
                if (!uiManager.IsGameOver && !stopMenuManager.IsPaused)
                    Jump();
            };
        }

        private void Update()
        {
            Rotation();
        }


        private void OnEnable()
        {
            _jumpInput.Enable();
        }

        private void OnDisable()
        {
            _jumpInput.Disable();
        }

        private void Jump()
        {
            var jumpInput = _rb.linearVelocity;
            jumpInput.y = 0;
            _rb.linearVelocity = jumpInput;
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger(OnFly);
            if (audioSource != null && audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }

        public void SetJump()
        {
            _jumpInput.Disable();
        }

        private void Rotation()
        {
            // Ziskame rychlost na ose y
            float velocity = _rb.linearVelocity.y;
            
            // Podle rychjlosti urcime cilovy uhle
            float targetAngel;
            if (velocity > 0)
            {
                targetAngel = maxUpAngle;
            }
            else
            {
                targetAngel = maxDownAngle;
            }
            
            // Plynula rotace mezi soucasnym a cilovym uhlem
            float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngel, rotationSpeed * Time.deltaTime);
            // Nastavime rotaci
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
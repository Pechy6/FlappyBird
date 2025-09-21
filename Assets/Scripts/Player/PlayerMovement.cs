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
    
        // Manager
        private PlayerManager _playerManager;
        [SerializeField] private UiManager uiManager;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerManager = GetComponent<PlayerManager>();
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
            _jumpInput.performed += ctx =>
            { if(!uiManager.IsGameOver)
                Jump();
            };
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
        }

        public void SetJump()
        { 
            _jumpInput.Disable();
        }
    }
}
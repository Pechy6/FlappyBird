using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class BirdMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
    
        // Jump 
        private InputAction _jumpInput;
        [SerializeField] private float jumpForce;
    
        // Manager
        private PlayerManager _playerManager;

        
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
            _jumpInput.performed += ctx =>
            { 
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
            OnDisable();
        }
    }
}
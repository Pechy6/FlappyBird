using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    // Jump 
    private InputAction _jumpInput;
    [SerializeField] private float jumpForce;
    
    // Crash
    private PlayerCollision _playerCollision;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerCollision = GetComponent<PlayerCollision>();
        if (_playerCollision == null)
        {
            Debug.LogWarning("PlayerCollision not found.");
        }
        
        _jumpInput = new InputAction("Jump", binding: "<Keyboard>/space");
    }

    private void Start()
    {
        _jumpInput.performed += ctx =>
        {
            if (!_playerCollision.IsCrashed)
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
}

using System;
using UnityEngine;

namespace Environment
{
    public class MovingGround : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float moveSpeed = 10f;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                Debug.LogWarning("Rigidbody2D not found.");
            }
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _rb.transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("DestroyGameObject"))
            {
                Destroy(gameObject);
            }
        }
    }
}
using UnityEngine;
namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private int _indexOfObstacleMask;
        public bool IsCrashed { get; private set; }
        private PlayerManager _playerManager;
        private void Awake()
        {
            _indexOfObstacleMask = LayerMask.NameToLayer("Obstacle");
            IsCrashed = false;
            _playerManager = GetComponent<PlayerManager>();
            if (_playerManager == null)
            {
                Debug.LogWarning("PlayerCollision not found.");
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer != _indexOfObstacleMask) return;
            _playerManager.OnDeath();
            Debug.Log("Game Over");
        }

        public void SetCrashed(bool value)
        {
            IsCrashed = value;
        }
    }
}
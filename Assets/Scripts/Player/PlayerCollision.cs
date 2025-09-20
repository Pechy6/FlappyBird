using UnityEngine;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private int _indexOfObstacleMask;
        public bool IsCrashed { get; private set; }
        private void Awake()
        {
            _indexOfObstacleMask = LayerMask.NameToLayer("Obstacle");
            IsCrashed = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer != _indexOfObstacleMask) return;
            IsCrashed = true;
            Debug.Log("Game Over");
        }
    }
}
using System;
using UnityEngine;

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
        if (other.gameObject.layer == _indexOfObstacleMask)
        {
            IsCrashed = true;
            Debug.Log("Game Over");
        }
    }
}
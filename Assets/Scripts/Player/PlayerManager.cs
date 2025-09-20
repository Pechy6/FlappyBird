using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private int _score;

        private void Awake()
        {
            _score = 0;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("AddScore"))
            {
                _score++;
                Debug.Log("Add to score" + _score);
            }
        }
    }
}
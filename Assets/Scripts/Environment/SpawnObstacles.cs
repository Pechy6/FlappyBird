using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Environment
{
    public class SpawnObstacles : MonoBehaviour
    {
        [SerializeField] GameObject pipePrefab;
        [SerializeField] float spawnIntervalStart = 1;
        [SerializeField] float spawnIntervalEnd = 3;
        [SerializeField] float minHeightOffset = 1f;
        [SerializeField] float heightOffset = 1f;

        private float _spawnIn;
        private float _timer;
        

        private void Update()
        {
            TimeToSpawn();
        }

        private void TimeToSpawn()
        {
            _timer += Time.deltaTime;
            if (_timer > _spawnIn)
            {
                SpawnPipe();
                _timer = 0;
                IntervalToSpawn();
            }
        }

        private void IntervalToSpawn()
        {
            _spawnIn = UnityEngine.Random.Range(spawnIntervalStart, spawnIntervalEnd);
        }

        private void SpawnPipe()
        {
            float randomY = UnityEngine.Random.Range(-minHeightOffset, heightOffset);
            Vector3 spawnPoints = transform.position + new Vector3(0, randomY, 0);
            Instantiate(pipePrefab, spawnPoints, Quaternion.identity);
        }
    }
}
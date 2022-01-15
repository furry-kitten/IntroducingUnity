using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Base
{
    public class Spawner : MonoBehaviour
    {
        public GameObject SpawnedObject;
        public List<Vector3> SpawnPoints;
        public float Periodicity;
        public int PackCount = -1;
        public int NumberOfEnemiesInPack = -1;
        public bool CanSpawn;

        private float time = 0;
        private List<GameObject> allSpawnedObject = new List<GameObject>();

        // Start is called before the first frame update
        private void Start() {

        }

        // Update is called once per frame
        private void Update() {
            Spawn();
        }

        private void Spawn() {
            CanSpawn &= PackCount > 0;
            if (CanSpawn == false) {
                return;
            }

            NumberOfEnemiesInPack = SpawnPoints.Count;
            time += Time.deltaTime;
            if (time >= Periodicity) {
                SpawnPack();
                PackCount--;

                time = 0;
            }
        }

        private void SpawnPack() {
            for (var i = 0; i < NumberOfEnemiesInPack; i++) {
                SpawnEnemy(SpawnPoints[i]);
            }
        }

        private void SpawnEnemy(Vector3 spawnPoint) {
            GameObject instantiate = Instantiate(SpawnedObject, spawnPoint, Quaternion.identity);
            instantiate.name = $"{SpawnedObject.name} {allSpawnedObject.Count}";
            allSpawnedObject.Add(instantiate);
        }
    }
}

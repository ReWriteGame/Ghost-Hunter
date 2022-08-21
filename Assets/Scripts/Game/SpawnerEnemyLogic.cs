using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class SpawnerEnemyLogic : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;
        [SerializeField] [Min(0)] private int spawnCount;
        [SerializeField] Vector2 delayВetweenSpawns = Vector2.one;

        public List<GameObject> spawnedObjects;

        private int spawnedCount = 0;


        private void FixedUpdate()
        {
            if (spawnedCount < spawnCount)
                StartCoroutine(StartSpawnRoutine());
        }

        private void Start()
        {
            spawner.OnSpawnPrefabEvent.AddListener(SpawnedObject);
        }

        private void SpawnObject()
        {
            spawner.SpawnRandomPrefab();
        }

        private void SpawnedObject(GameObject obj)
        {
            spawnedObjects.Add(obj);
            obj.GetComponent<Enemy>().OnDestroyObj += DestroyObject;
        }


        private void DestroyObject(GameObject obj)
        {
            spawnedCount -= 1;
            spawnedObjects.Remove(obj);
            obj.GetComponent<Enemy>().OnDestroyObj -= DestroyObject;
        }

        private IEnumerator StartSpawnRoutine()
        {
            spawnedCount += 1;
            yield return new WaitForSeconds(Random.Range(delayВetweenSpawns.x, delayВetweenSpawns.y));
            SpawnObject();
        }

     
    }
}
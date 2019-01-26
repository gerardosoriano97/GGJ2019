using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    IEnumerator spawnRoutine;

    public float radius = 10.0f;
    public float spawnDelay = 2.0f;
    public float spawnVariation = 1.0f;
    public GameObject prefab;
    public int poolSize = 10;
    public List<GameObject> pool;
    void Start()
    {
        pool = new List<GameObject>(); 
        PopulatePool();

        spawnRoutine = SpawnRoutine();
        StartCoroutine(spawnRoutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnRoutine() {
        while (true) {
            Vector3 spawnPoint = transform.position;
            Vector2 randPoint = Random.insideUnitCircle;
            spawnPoint.x = randPoint.x;
            spawnPoint.z = randPoint.y;
            Vector3 dir = transform.position - spawnPoint;
            dir.Normalize();
            GameObject go = GetFromPool();
            if (go != null) {
                go.transform.position = dir * radius;
            }
            yield return new WaitForSeconds(spawnDelay + Random.Range(-spawnVariation, spawnVariation));
        }
    }

    public GameObject GetFromPool() {
        for (int i = 0; i < poolSize; i++) {
            if (!pool[i].activeInHierarchy) {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }

    public void PopulatePool() {
        for (int i = 0; i < poolSize; i++) {
            GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            go.transform.parent = transform;
            go.SetActive(false);
            pool.Add(go);
        }
    }
}

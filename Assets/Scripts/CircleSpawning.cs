using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawning : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] circlePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCircles();
    }

    void SpawnCircles()
    {
        if (Input.touchCount > 0)
        {
            int randomCircle = Random.Range(0, circlePrefabs.Length);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(circlePrefabs[0], spawnPoints[randomSpawnPoint].position, transform.rotation);
        }
    }
}

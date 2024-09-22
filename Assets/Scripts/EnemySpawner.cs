using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject asteroidPrefab;
     
    public float spawnRatePerMinute = 30f;

    public float spawnRateIncrement = 1f;

    public float xlimit = 10;

    private float spawnNext = 0;

    public float maxLifeTime = 3f;

    // Update is called once per frame
    void Update()
{
    if (Time.time > spawnNext)
    {
        spawnNext = Time.time + 60 / spawnRatePerMinute;

        spawnRatePerMinute += spawnRateIncrement;

        float rand = Random.Range(-xlimit, xlimit);
        Vector2 spawnPosition = new Vector2(rand, 8f);

        // Obtén el meteorito del pool
        GameObject meteor = ObjectPoolMeteorito.SharedInstance.GetPooledObjectM();

        if (meteor != null)
        {
            // Posiciona el meteorito en la posición de spawn y actívalo
            meteor.transform.position = spawnPosition;
            meteor.transform.rotation = Quaternion.identity; // Resetea la rotación si es necesario
            meteor.GetComponent<Meteorito>().CambiarTamaño(2); // Cambia el tamaño del meteorito
            meteor.SetActive(true);
        }
    }
}

    
}

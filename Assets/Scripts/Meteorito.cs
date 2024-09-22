using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Meteorito : MonoBehaviour
{
    public float tamaño = 2; // Valor predeterminado del tamaño

    public GameObject asteroidPrefab;

    public float maxLifeTime = 3f;

    public float thrustForce = 5;

    public float rotar = 10f;

    public float separacion = 0.5f;
    void OnEnable()
    {
        // Ajusta la escala en función del tamaño
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(0.4f * tamaño/2, 0.4f * tamaño/2, 1);
        rotar = Random.Range(10f, 80f);
        // Reiniciar velocidad lineal (movimiento)
        GetComponent<Rigidbody>().velocity = Vector3.zero;

// Reiniciar velocidad angular (rotación)
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

// Reiniciar fuerzas acumuladas
        GetComponent<Rigidbody>().inertiaTensorRotation = Quaternion.identity; // Restablece el tensor de inercia
        GetComponent<Rigidbody>().ResetInertiaTensor(); // Resetea el tensor de inercia

// Si quieres reiniciar la posición o rotación:
        GetComponent<Rigidbody>().rotation = Quaternion.identity; // Opcional, restablece la rotación
    }
    private void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.CompareTag("Bala"))
    {
        if (tamaño == 1)
        {
            // Si el tamaño es 1, destruye el meteorito y la bala
            IncreaseScore();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
       else
{
    // Si el tamaño es mayor que 1, reduce su tamaño y destruye la bala
    tamaño--; // Primero reduce el tamaño

    // Ahora aumenta el puntaje
    IncreaseScore();
    Vector2 spawnPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    // Luego ajusta la escala en función del nuevo tamaño

    // Separación de meteoritos
    GameObject hijo1 = ObjectPoolMeteorito.SharedInstance.GetPooledObjectM(); // Obtén el primer meteorito del pool
    if (hijo1 != null) 
    {
        Vector2 spawnPositionHijo1 = new Vector2(spawnPosition.x - separacion, spawnPosition.y);
        hijo1.transform.position = spawnPositionHijo1;        hijo1.transform.rotation = Quaternion.identity;
        hijo1.GetComponent<Meteorito>().CambiarTamaño(1); // Cambia el tamaño del meteorito
        hijo1.SetActive(true); // Activa el objeto
        Vector3 direccionBase = transform.right;

        // Creamos una rotación en torno al eje Z (puedes cambiarlo si es necesario) usando el valor de "rotar"
        Quaternion rotacion = Quaternion.Euler(0, 0, -rotar - 90f);

        // Aplicamos la rotación a la dirección base
        Vector3 direccionRotada = rotacion * direccionBase;

        // Ahora aplicamos la fuerza en la dirección rotada
        hijo1.GetComponent<Rigidbody>().AddForce(direccionRotada * thrustForce);
        hijo1.transform.up = direccionRotada*-1f;
    }

    // Creamos el segundo hijo
    GameObject hijo2 = ObjectPoolMeteorito.SharedInstance.GetPooledObjectM(); // Obtén el segundo meteorito del pool
    if (hijo2 != null)
    {
        Vector2 spawnPositionHijo2 = new Vector2(spawnPosition.x + separacion, spawnPosition.y);
        hijo2.transform.position = spawnPositionHijo2;
        hijo2.transform.rotation = Quaternion.identity;
        hijo2.GetComponent<Meteorito>().CambiarTamaño(1); // Cambia el tamaño del meteorito
        hijo2.SetActive(true); // Activa el objeto
        Vector3 direccionBase = transform.right;

        // Rotamos la dirección base en sentido contrario
        Quaternion rotacion = Quaternion.Euler(0, 0, rotar - 90f);
        Vector3 direccionRotada = rotacion * direccionBase;
        hijo2.GetComponent<Rigidbody>().AddForce(direccionRotada * thrustForce);
        hijo2.transform.up = direccionRotada*-1f;
    }

    // Desactiva el meteorito original y la bala para volver a meterlos en el pool
    collision.gameObject.SetActive(false);
    gameObject.SetActive(false);
}

    }
}


    private void IncreaseScore ()
    {
        Player.SCORE ++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectsWithTag("UI")[0];
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }

    public void CambiarTamaño(float nuevoTamaño)
    {
        tamaño = nuevoTamaño;
        transform.localScale = new Vector3(0.4f * tamaño/2, 0.4f * tamaño/2, 1);
    }
}

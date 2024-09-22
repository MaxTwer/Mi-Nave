using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    public float maxLifeTime = 3f;
    private float lifeTime;
    public Vector3 targetVector;

    // Start is called before the first frame update
    public void OnEnable()
    {
        // Reinicia el tiempo de vida cuando el objeto es activado
        lifeTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime );
        lifeTime += Time.deltaTime;

        // Si el objeto ha excedido su tiempo de vida, se desactiva en vez de destruirse
        if (lifeTime >= maxLifeTime)
        {
            // Desactiva el objeto y lo devuelve al pool
            gameObject.SetActive(false);
        }
    }

}


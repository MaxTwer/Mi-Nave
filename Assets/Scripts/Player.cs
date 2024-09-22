using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody _rigid;

    public float thrustForce = 5;

    public float rotationSpeed = 10f;

    public GameObject gun, bulletPrefab;

    public static int SCORE = 0;

    public float xBorderLimit = 9.5f;

    public float yBorderLimit = 5.5f;

    [SerializeField] private GameObject pausado;
    [SerializeField] private GameObject pausad1;
    [SerializeField] private GameObject pausad2;
    [SerializeField] private GameObject reinicio;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
        {
            newPos.x = -xBorderLimit+1;
        }
        else if (newPos.x < -xBorderLimit)
        {
            newPos.x = xBorderLimit-1;
        }
        if (newPos.y > yBorderLimit)
        {
            newPos.y = -yBorderLimit+1;
        }
        else if (newPos.y < -yBorderLimit)
        {
            newPos.y = yBorderLimit;
        }
        transform.position = newPos;

        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, rotation * rotationSpeed * -1);

        if(Input.GetKeyDown(KeyCode.Space)){

            GameObject bullet = ObjectPoolBala.SharedInstance.GetPooledObjectB(); 
            if (bullet != null) {
                bullet.transform.position = gun.transform.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
            }
            Bullet balascript = bullet.GetComponent<Bullet>();

            balascript.targetVector = transform.right;
        }

        
        if(Input.GetKeyDown(KeyCode.Escape)){
            
        if(Time.timeScale == 0){
            Time.timeScale = 1;
            pausado.SetActive(false);
            pausad1.SetActive(false);
            pausad2.SetActive(false);
            reinicio.SetActive(false);

        }   
        else{
            Time.timeScale = 0;
            pausado.SetActive(true);
            pausad1.SetActive(true);
            pausad2.SetActive(true);
            reinicio.SetActive(true);
        }
    }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")){
            SCORE=0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            Debug.Log("Colision con otra cosa");   
        }
    }

    
}

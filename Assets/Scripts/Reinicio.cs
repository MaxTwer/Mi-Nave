using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    
public class Reinicio : MonoBehaviour
{
    [SerializeField] private GameObject pausado;
    [SerializeField] private GameObject pausad1;
    [SerializeField] private GameObject pausad2;
    [SerializeField] private GameObject reinicio;

    public void Reiniciar()
    {
        Player.SCORE = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
            pausado.SetActive(false);
            pausad1.SetActive(false);
            pausad2.SetActive(false);
            reinicio.SetActive(false);
    }
}

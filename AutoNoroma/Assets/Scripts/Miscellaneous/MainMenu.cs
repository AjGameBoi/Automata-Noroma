using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame (II)");
    }

    public void QuitGame()
    {
        Debug.Log("Sayonara!");
        Application.Quit();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

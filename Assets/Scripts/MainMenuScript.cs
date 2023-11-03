using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load3x3Screen()
    {
        SceneManager.LoadSceneAsync("3x3");

    }
    public void Load4x4Screen()
    {
        SceneManager.LoadSceneAsync("4x4");

    }
    public void Load5x5Screen()
    {
        SceneManager.LoadSceneAsync("5x5");

    }
    public void Load6x6Screen()
    {
        SceneManager.LoadSceneAsync("6x6");

    }
    public void Load7x7Screen()
    {
        SceneManager.LoadSceneAsync("7x7");

    }
}

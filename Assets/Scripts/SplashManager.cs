using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadSceneAsync("Lobby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

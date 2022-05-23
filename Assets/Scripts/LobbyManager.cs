using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageName;
    void Start()
    {
        if (StageManager.Instance == null)
        {
            SceneManager.LoadScene("Splash");
            return;
        }
        stageName.text = "Stage:" + StageManager.Instance.GetStageName();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectStage(bool next)
    {
        stageName.text = "Stage:"+StageManager.Instance.SelectStage(next);
    }
}

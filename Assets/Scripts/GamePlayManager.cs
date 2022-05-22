using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    const string STAGE_DATA_FOLDER = "Stage";
    const string STAGE_DATA_PREFIX = "Stage_";
    const string STAGE_DATA_EXTENSION = ".csv";
    const int STAGE_LINE_COUNT = 9;

    [SerializeField] GameObject brickPrefab;
    StageManager stageManager;
    List<Brick> bricks;
    // Start is called before the first frame update
    void Start()
    {
        stageManager = StageManager.Instance;
        string stagePath = Path.Combine(Application.streamingAssetsPath, STAGE_DATA_FOLDER,
            STAGE_DATA_PREFIX + stageManager.GetStageID()+ STAGE_DATA_EXTENSION);
        string xml = File.ReadAllText(stagePath);
        string[] lines = xml.Split('\n');
        for (int i = 0; i < STAGE_LINE_COUNT; i++)
        {
            //Debug.Log(lines[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("Lobby");
        }
    }
}

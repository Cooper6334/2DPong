using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    const float BRICK_START_X = -2;
    const float BRICK_START_Y = 2;
    const float BRICK_WIDTH = 0.5f;
    const float BRICK_HEIGHT = 0.2f;

    [SerializeField] GameObject brickPrefabBasic;
    StageManager stageManager;
    int[][] bricks;
    // Start is called before the first frame update
    void Start()
    {
        stageManager = StageManager.Instance;
        if (stageManager == null)
        {
            SceneManager.LoadScene("Splash");
            return;
        }
        bricks = stageManager.GetCurrentStageBricks();

        for (int c = 0; c < StageManager.STAGE_COLUMN_COUNT; c++)
        {
            for (int r = 0; r < StageManager.STAGE_ROW_COUNT; r++)
            {
                GameObject brickPrefab = null;
                switch (bricks[c][r])
                {
                    case 0:
                    default:
                        break;
                    case 1:
                        brickPrefab = brickPrefabBasic;
                        break;
                }
                if (brickPrefab != null)
                {
                    GameObject newBrick = Instantiate(brickPrefab);
                    newBrick.transform.position = new Vector2(
                        BRICK_START_X + r * BRICK_WIDTH,
                        BRICK_START_Y - c * BRICK_HEIGHT
                        );
                }
            }
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

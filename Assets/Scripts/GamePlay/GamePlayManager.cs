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
    const int DEFAULT_LIFE_COUNT = 3;

    [SerializeField] GameObject brickPrefabBasic;

    [SerializeField] Transform brickContainer;
    [SerializeField] Player player;
    [SerializeField] Ball ball;

    StageManager stageManager;
    int[][] bricks;
    int brickCount = 0;

    int lifeCount;
    bool isGamePlaying = false;


    static GamePlayManager _instance;
    public static GamePlayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GamePlayManager>();
            }
            return _instance;
        }
    }

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
                    brickCount++;
                    GameObject newBrick = Instantiate(brickPrefab, brickContainer);
                    newBrick.transform.position = new Vector2(
                        BRICK_START_X + r * BRICK_WIDTH,
                        BRICK_START_Y - c * BRICK_HEIGHT
                        );
                }
            }
        }
        lifeCount = DEFAULT_LIFE_COUNT;
        CreateBall();
        isGamePlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("Lobby");
        }

        if (isGamePlaying)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ball.Shoot();
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                ball.Reset();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                player.SetDirection(Player.Direction.Left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                player.SetDirection(Player.Direction.Right);
            }
            else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                player.SetDirection(Player.Direction.Stay);
            }
        }
    }
    bool CreateBall()
    {
        if(lifeCount <= 0)
        {
            return false;
        }
        lifeCount--;
        ball.Reset();
        return true;
    }

    void StageClear()
    {
        Debug.Log("StageClear");
        Destroy(ball.gameObject);
        isGamePlaying = false;
        ShowUI();
    }

    void GameOver()
    {
        Debug.Log("GameOver");
        Destroy(ball.gameObject);
        isGamePlaying = false;
        ShowUI();
    }

    public void ShowUI()
    {

    }

    #region event
    public void OnBrickDestroy()
    {
        brickCount--;
        if (brickCount <= 0)
        {
            StageClear();
        }
    }

    public void OnBallFall()
    {
        if (!CreateBall())
        {
            GameOver();
        }
    }
    #endregion
}

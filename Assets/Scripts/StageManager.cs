using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    const string STAGE_DATA_FOLDER = "Stage";
    const string STAGE_DATA_PREFIX = "Stage_";
    const string STAGE_DATA_EXTENSION = ".csv";

    const int FIRST_STAGE = 0;
    const int LAST_STAGE = 3;

    public const int STAGE_ROW_COUNT = 9;
    public const int STAGE_COLUMN_COUNT = 6;

    static StageManager _instance;
    public static StageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StageManager>();
            }
            return _instance;
        }
    }

    int stageID = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public string SelectStage(bool next)
    {
        if (next)
        {
            if (stageID < LAST_STAGE)
            {
                stageID++;
            }
        }
        else
        {
            if(stageID > FIRST_STAGE)
            {
                stageID--;
            }
        }
        return GetStageName();
    }

    public string GetStageName()
    {
        if(stageID == FIRST_STAGE)
        {
            return "Tutorial";
        }
        else
        {
            return "" + stageID;
        }
    }

    public bool IsTutorial()
    {
        return stageID == FIRST_STAGE;
    }

    public bool HaveNextStage()
    {
        return stageID < LAST_STAGE;
    }

    public int GetStageID()
    {
        return stageID;
    }

    void InstantiateStage()
    {

    }

    public int[][] GetCurrentStageBricks()
    {
        int[][] result = new int[STAGE_COLUMN_COUNT][];
        string stagePath = Path.Combine(Application.streamingAssetsPath, STAGE_DATA_FOLDER,
            STAGE_DATA_PREFIX + stageID + STAGE_DATA_EXTENSION);
        string xml = File.ReadAllText(stagePath);
        string[] lines = xml.Split('\n');
        for (int c = 0; c < STAGE_COLUMN_COUNT; c++)
        {
            result[c] = new int[STAGE_ROW_COUNT];
            string[] column = lines[c].Split(',');
            for (int r = 0; r < STAGE_ROW_COUNT; r++)
            {
                result[c][r] = int.Parse(column[r]);
            }
        }
        return result;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class StageManager : MonoBehaviour
{
    const string STAGE_DATA_PREFIX = "stage_";
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
            if (stageID > FIRST_STAGE)
            {
                stageID--;
            }
        }
        return GetStageName();
    }

    public string GetStageName()
    {
        if (stageID == FIRST_STAGE)
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

    public IEnumerator GetStageFromFile(Action<int[][]> callback)
    {
        int[][] result = new int[STAGE_COLUMN_COUNT][];
        string stagePath = Path.Combine(Application.streamingAssetsPath,
            STAGE_DATA_PREFIX + stageID + STAGE_DATA_EXTENSION);
        UnityWebRequest www = UnityWebRequest.Get(stagePath);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError
            || www.result == UnityWebRequest.Result.DataProcessingError
            || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Load stage Error " + www.error);
        }
        string csv = www.downloadHandler.text;
        csv = csv.Replace((char)65279 + "", string.Empty);
        string[] lines = csv.Split('\n');
        for (int c = 0; c < STAGE_COLUMN_COUNT; c++)
        {
            result[c] = new int[STAGE_ROW_COUNT];
            string[] column = lines[c].Split(',');
            for (int r = 0; r < STAGE_ROW_COUNT; r++)
            {
                result[c][r] = int.Parse(column[r]);
            }
        }
        yield return result;
        callback(result);
    }
}

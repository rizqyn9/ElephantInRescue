using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] string saveFilePath;

    public PlayerDataModel playerDataModel => GameManager.Instance.playerDataModel;

    public PlayerDataModel load()
    {
        saveFilePath = Dev.Instance.isDevMode ?
            Application.dataPath + "/eir_dev.json" :
            Application.persistentDataPath + "/eir_production.json";

        if (Dev.Instance.isDevMode && Dev.Instance.useCustomUserModel) return Dev.Instance.m_customPlayerModel;
        else if (Dev.Instance.isDevMode && Dev.Instance.useNewDataUser) return new PlayerDataModel();
        else if (File.Exists(saveFilePath))
        {
            try
            {
                PlayerDataModel res = JsonUtility.FromJson<PlayerDataModel>(File.ReadAllText(saveFilePath));
                return res;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return GenerateNewData();
            }
        }
        else return GenerateNewData();
    }

    public void Save()
    {
        try
        {
            File.WriteAllText(saveFilePath, JsonUtility.ToJson(playerDataModel));
            Debug.Log("<color=green>Success update data</color>");
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    PlayerDataModel GenerateNewData() =>
        new PlayerDataModel()
        {
            LevelDatas = new List<LevelDataModel>()
            {
                new LevelDataModel()
                {
                    Level = 1,
                    Stage = 1,
                    IsOpen = true,
                    Stars = 0,
                    IsNewLevel = true,
                    HighScore = 0
                },
                new LevelDataModel()
                {
                    Level = 1,
                    Stage = 2,
                    IsOpen = true,
                    Stars = 0,
                    IsNewLevel = true,
                    HighScore = 0
                }
            }
        };
}


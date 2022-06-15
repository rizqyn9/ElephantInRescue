using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] string saveFilePath;

    public PlayerDataModel PlayerDataModel { get => GameManager.PlayerDataModel; }

    [SerializeField] PlayerDataModel m_debug;
    public PlayerDataModel Load()
    {
        saveFilePath = Application.persistentDataPath + "/eir_production.json";

        if (Dev.Instance.isDevMode && Dev.Instance.useCustomUserModel) return Dev.Instance.m_customPlayerModel;
        else if (Dev.Instance.isDevMode && Dev.Instance.useNewDataUser) return new PlayerDataModel();
        else if (File.Exists(saveFilePath))
        {
            try
            {
                Debug.Log("<color=green>Load persistant data</color>");
                PlayerDataModel res = JsonUtility.FromJson<PlayerDataModel>(File.ReadAllText(saveFilePath));
                m_debug = res;
                return res;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return GenerateNewData();
            }
        }
        else
        {
            print("<color=red>Generate New data</color>");
            return GenerateNewData();
        }
    }

    public void Save()
    {
        try
        {
            File.WriteAllText(saveFilePath, JsonUtility.ToJson(PlayerDataModel));
            Debug.Log("<color=green>Success update data</color>");
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    // TODO
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
# if UNITY_EDITOR
                new LevelDataModel()
                {
                    Level = 1,
                    Stage = 2,
                    IsOpen = true,
                    Stars = 0,
                    IsNewLevel = true,
                    HighScore = 0
                }
#endif
            }
        };
}


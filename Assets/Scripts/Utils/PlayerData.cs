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
        try
        {
            saveFilePath = Application.persistentDataPath + "/eir_beta1.json";

            if (File.Exists(saveFilePath))
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
                    throw e;
                }
            }
            else
            {
                throw new System.Exception("JSON not found");
            }
        } catch (System.Exception e)
        {
            Debug.LogWarning(e);
            print("<color=red>Generate New data</color>");
            PlayerDataModel newPlayerData = GenerateNewData();
            Save(newPlayerData);
            return newPlayerData;
        }
    }

    public void Save(PlayerDataModel playerDataModel = new PlayerDataModel())
    {
        try
        {
            if(playerDataModel.Equals(default(PlayerDataModel)))
                File.WriteAllText(saveFilePath, JsonUtility.ToJson(PlayerDataModel));
            else
                File.WriteAllText(saveFilePath, JsonUtility.ToJson(playerDataModel));

            Debug.Log("<color=green>Success update data</color>");
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    // TODO
    PlayerDataModel GenerateNewData()
    {
        List<LevelDataModel> generated = new List<LevelDataModel>();

        for(int level = 1; level < 4; level++)
            for(int stage = 1; stage < 4; stage++)
#if UNITY_EDITOR
                if (level == 1 && stage <= 2) // Change this for development only
#else
                if (level == 1 && stage == 1)
#endif
                    generated.Add(GenerateLevelData(stage, level, true, true));
                else 
                    generated.Add(GenerateLevelData(stage, level));

        return new PlayerDataModel() { LevelDatas = generated };
    }

    LevelDataModel GenerateLevelData(int stage, int level, bool isNewLevel = true, bool isOpen = false) =>
        new LevelDataModel
        {
            Stage= stage,
            Level = level,
            IsNewLevel = isNewLevel,
            IsOpen = isOpen,
            Stars = 0,
            HighScore = 0
        };
}


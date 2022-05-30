using System.IO;
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
                return new PlayerDataModel();
            }
        }
        else return new PlayerDataModel();
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

    PlayerDataModel getNewUserTemp() =>
        new PlayerDataModel();
}


using System.IO;
using UnityEngine;

namespace EIR
{
    public class PlayerData : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] string saveFilePath;

        public PlayerDataModel load()
        {
            saveFilePath = Dev.Instance.isDevMode ?
                Application.dataPath + "/eir_dev.json" :
                Application.persistentDataPath + "/eir_production.json";

            if (Dev.Instance.isDevMode && Dev.Instance.useCustomUserModel) return Dev.Instance.customPlayerModel;
            else if (File.Exists(saveFilePath))
            {
                try
                {
                    return JsonUtility.FromJson<PlayerDataModel>(File.ReadAllText(saveFilePath));
                } catch (System.Exception e)
                {
                    print(e);
                    return new PlayerDataModel();
                }
            }
            else return new PlayerDataModel();   
        }

        public void save()
        {

        }
    }
}

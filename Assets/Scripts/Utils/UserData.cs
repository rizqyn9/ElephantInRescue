using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR
{
    public class UserData : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] string saveFilePath;

        public void init()
        {
            saveFilePath = Application.persistentDataPath + "/aljava.json";

            // use custom data
            if (Dev.Instance.isDevMode && Dev.Instance.useCustomUserModel) GameManager.Instance.userModel = Dev.Instance.customUserModel;
            else
            {

            }
        }
    }
}

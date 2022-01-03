using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR
{
    public class Dev : MonoBehaviour
    {
        #region Singleton
        private static Dev _instance;
        public static Dev Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Dev>();
                    if (_instance == null) print("Dev not Instanced");
                }
                return _instance;
            }
            private set { }
        }
        #endregion

        [Header("Properties")]
        public bool isDevMode = true;
        public bool useCustomUserModel = true;
        public UserModel customUserModel;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else Destroy(gameObject);
        }

        private void Start()
        {
            if (isDevMode)
            {
                print("<color=red> Dev Mode</color>");
            }
        }
    }
}

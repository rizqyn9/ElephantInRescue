using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR
{
    public class GameManager : MonoBehaviour
    {
        [Header("Properties")]
        public UserData userData;


        [Header("Debug")]
        public UserModel userModel = new UserModel();

        // Accessors
        public static UserData UserData => Instance.userData;


        #region Singleton
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null) print("<color=red>Game Manager not instance</>");
                }
                return _instance;
            }
            private set { }
        }
        #endregion

        /**
         * Load Game 
         */
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
            userModel = new UserModel(); // Resset User State

            StartCoroutine(ILoadAllResources());
        }

        IEnumerator ILoadAllResources()
        {
            userData.init();
            yield return new WaitWhile(() => userModel.Equals(default(UserModel)));
            print("User Data Loaded");

            print("Game Started");
            yield break;
        }
    }
}

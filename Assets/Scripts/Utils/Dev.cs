using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using EIR.Game;

namespace EIR
{
    public class Dev : MonoBehaviour
    {
        #region Singleton
        private static Dev _instance;
        public static Dev Instance { get => _instance; private set { } }
        #endregion

        [Header("Properties")]
        public bool isDevMode = true;
        public bool useCustomUserModel = true;
        public bool useNewDataUser = false;
        public PlayerDataModel customPlayerModel;
        public bool useLevelTest = true;
        public LevelBase levelTest;
        public GameObject gameManagerPrefab;

        private void Awake()
        {
            if (_instance != null) Destroy(gameObject);
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            StartCoroutine(IDevStart());
        }

        IEnumerator IDevStart()
        {
            if (isDevMode)
            {
                if (GameManager.Instance == null) Instantiate(gameManagerPrefab);
                print("<color=red> Dev Mode</color>");
            }

            yield return new WaitUntil(() => GameManager.Instance != null);

            if (GameManager.SceneState == SceneState.MAINMENU)
                GameManager.Instance.initialize();
            else
            {
                LevelManager.Instance.Init(levelTest);

            }
        }
    }
}

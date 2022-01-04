using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace EIR
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager _instance;
        public static GameManager Instance { get => _instance; }
        #endregion

        [Header("Properties")]
        public PlayerData playerData;
        public GameObject GO_ResourcesManager, GO_MainController;

        [Header("Debug")]
        public PlayerDataModel playerDataModel = new PlayerDataModel();
        [SerializeField] SceneState _sceneState = SceneState.MAINMENU;
        [SerializeField] ResourcesManager resourcesManager;
        [SerializeField] MainController mainController;

        // Accessors
        public static PlayerData PlayerData => Instance.playerData;
        public static SceneState SceneState => updateSceneState();


        private void Awake()
        {
            if (_instance != null) Destroy(gameObject);
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        /**
         * Use dev script for starting in dev mode
         */
        private void Start()
        {
            if (Dev.Instance.isDevMode) return;
            else initialize();
        }

        public void initialize()
        {
            updateSceneState();
            StartCoroutine(ILoadAllResources());

            playerDataModel = playerData.load();


            validateNewUser();
        }

        private void validateNewUser()
        {
            if (SceneState == SceneState.GAME && Dev.Instance.isDevMode) return;
            if (playerDataModel.Equals(default(PlayerDataModel)))
                MainMenuManager.Instance.spawnForm();
            else
                MainMenuManager.UI_MainMenu.setUserName(playerDataModel.userName);
        }

        /**
         * Load all dependencies
         * Resources Manager
         * Main Controller
         * 
         */
        IEnumerator ILoadAllResources()
        {
            if (ResourcesManager.Instance == null)
                resourcesManager = Instantiate(GO_MainController).GetComponent<ResourcesManager>();
            else resourcesManager = ResourcesManager.Instance;

            if (MainController.Instance == null)
                mainController = Instantiate(GO_MainController).GetComponent<MainController>();
            else mainController = MainController.Instance;

            yield break;
        }

        private void OnEnable() =>
            SceneManager.sceneLoaded += handleSceneChanged;

        private void OnDisable() =>
            SceneManager.sceneLoaded -= handleSceneChanged;

        static SceneState updateSceneState()
        {
            Scene _scene = SceneManager.GetActiveScene();
            if (_scene.name == "MainMenu") return SceneState.MAINMENU;
            else return SceneState.GAME;
        }

        private void handleSceneChanged(Scene _scene, LoadSceneMode _loadMode)
        {
            print($" Load {_scene}");
        }

        public static void LoadMainMenu()
        {
            
        }

        public static void LoadGameLevel(LevelBase _levelBase)
        {

        }
    }
}

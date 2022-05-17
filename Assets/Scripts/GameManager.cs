using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using EIR.Game;
using EIR.MainMenu;

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
                MainMenuManager.UI_MainMenu.FormSetActive(true);
            else
                MainMenuManager.UI_MainMenu.UpdateUIComponent();
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
                resourcesManager = Instantiate(GO_ResourcesManager).GetComponent<ResourcesManager>();
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

        [SerializeField] LevelBase levelWillLoad;
        private void handleSceneChanged(Scene _scene, LoadSceneMode _loadMode)
        {
            print($" Load {_scene.name}");
        }

        /**
         * Back to main menu
         * Ensure all game environment clearance as properly
         */
        public static void LoadMainMenu()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        public static void LoadLevelMap()
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }

        /**
         * Load game level
         * Params reference to levelTarget with LevelBase 
         * Set levelWillLoad to targeted _levelBase
         */
        public static void LoadGameLevel(LevelBase _levelBase)
        {
            Instance.levelWillLoad = new LevelBase(); // Reset // Todo
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}

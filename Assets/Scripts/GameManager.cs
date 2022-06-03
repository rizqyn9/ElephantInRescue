using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    #endregion

    [Header("Properties")]
    public PlayerData playerData;
    public GameObject GO_ResourcesManager;

    [Header("Debug")]
    public PlayerDataModel playerDataModel = new PlayerDataModel() { LevelDatas = new List<LevelDataModel>()};
    [SerializeField] SceneState _sceneState = SceneState.MAINMENU;
    [SerializeField] ResourcesManager resourcesManager;

    public static PlayerData PlayerData { get => Instance.playerData; }
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

    private void Start()
    {
        if (Dev.Instance.isDevMode) return;
        else initialize();        
    }

    public void initialize()
    {
        updateSceneState();
        playerDataModel = playerData.load();
        StartCoroutine(ILoadAllResources());
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
        print($" Load {_scene.name}");

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    /**
    * Back to main menu
    * Ensure all game environment clearance as properly
    */
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        LeanTween.reset();
    }

    public static void LoadLevelMap()
    {
        print("Load Level");
//#if UNITY_EDITOR
//        LoadGameLevel(GetLevelDataByLevelStage(2, 1));
//        return;
//#endif
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    /**
    * Load game level
    * Params reference to levelTarget with LevelBase 
    * Set levelWillLoad to targeted _levelBase
    */
    [SerializeField] LevelDataModel m_levelLoaded;
    public LevelDataModel LevelDataModel { get => m_levelLoaded; private set => m_levelLoaded = value; }
    public static void LoadGameLevel(LevelDataModel level)
    {
        Instance.LevelDataModel = level; // Reset // Todo
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene($"Stg{level.Stage}-Lvl{level.Level}", LoadSceneMode.Additive);
    }

    /**
     * Close Application
     */
    public static void CloseApplication ()
    {
        // Do something
        Application.Quit();
    }

    public static LevelDataModel GetLevelDataByLevelStage(int level, int stage)
    {
        LevelDataModel levelDataModel= Instance.playerDataModel.LevelDatas.Find(val => val.Level == level && val.Stage == stage);
        if (levelDataModel.Equals(default(LevelDataModel)))
            return new LevelDataModel()
            {
                Level = level,
                Stage = stage,
                IsOpen = false,
                Stars = 0,
                IsNewLevel = false,
                HighScore = 0
            };
        else return levelDataModel;
    }
}


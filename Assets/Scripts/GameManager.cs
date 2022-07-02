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
    [SerializeField] SceneState _sceneState = SceneState.MAINMENU;
    PlayerDataModel m_playerDataModel;

    public static PlayerDataModel PlayerDataModel { get => Instance.m_playerDataModel; internal set => Instance.m_playerDataModel = value; }
    public static PlayerData PlayerData { get => Instance.playerData; }
    public static SceneState SceneState => UpdateSceneState();
    public static ResourcesManager ResourcesManager { get; set; }
    public SentryBehavior SentryBehavior { get; set; }

    private void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Load Resource data
    /// Load User data
    /// </summary>
    private void Start()
    {
        SentryBehavior = gameObject.AddComponent<SentryBehavior>();
        ResourcesManager = GetComponentInChildren<ResourcesManager>();
        Initialize();   
    }

    public void Initialize()
    {
        UpdateSceneState();
        PlayerDataModel = playerData.Load();
    }

    private void OnEnable() =>
        SceneManager.sceneLoaded += HandleSceneChanged;

    private void OnDisable() =>
        SceneManager.sceneLoaded -= HandleSceneChanged;

    static SceneState UpdateSceneState()
    {
        Scene _scene = SceneManager.GetActiveScene();
        if (_scene.name == "MainMenu") return SceneState.MAINMENU;
        else return SceneState.GAME;
    }

    private void HandleSceneChanged(Scene scene, LoadSceneMode loadMode)
    {
        PlayerDataModel = playerData.Load(); // Sync persistant data with runtime
        print($"Load {scene.name}");

        SoundManager.Instance.PlayBGM(scene.name);

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

#if UNITY_EDITOR
    [SerializeField] int levelDev;
    [SerializeField] int stageDev;
#endif
    public static void LoadLevelMap()
    {
//#if UNITY_EDITOR
//        LoadGameLevel(GetLevelDataByLevelStage(Instance.levelDev, Instance.stageDev));
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
    public static LevelSO LevelSO { get; private set; }
    public static void LoadGameLevel(LevelDataModel level)
    {
        try
        {
            LevelSO levelSO = LevelSO.FindLevel(ResourcesManager, level.Stage, level.Level);
            if (!levelSO) throw new System.Exception("Level target not found");
            LevelSO = levelSO;
            Instance.LevelDataModel = level; // Reset // Todo
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        } catch (System.Exception err)
        {
            // Handle coming soon level
            Debug.Log(err);
            LoadMainMenu();
        }
    }

    public static void LoadGameNextLevel(LevelSO current)
    {
        LevelSO next = ResourcesManager.ListLevels[ResourcesManager.ListLevels.FindIndex(level => level.Level == current.Level && level.Stage == current.Stage) + 1];
        LoadGameLevel(GetLevelDataByLevelStage(next.Level, next.Stage));
    }

    public static LevelSO GetLevelSO (int level, int stage) =>
        LevelSO.FindLevel(ResourcesManager, stage, level);

    /**
     * Close Application
     */
    public static void CloseApplication ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static LevelDataModel GetLevelDataByLevelStage(int level, int stage)
    {
        LevelDataModel levelDataModel= PlayerDataModel.LevelDatas.Find(val => val.Level == level && val.Stage == stage);
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

    public static void UpdatePlayerData(LevelDataModel levelDataModel)
    {
        int indexLevel = PlayerDataModel.LevelDatas.FindIndex(level => level.Level == levelDataModel.Level && level.Stage == levelDataModel.Stage);

        if (indexLevel < 0)
        {
            Debug.LogError($"Level not found {levelDataModel.ToString()}");
            return;
        }

        PlayerDataModel.LevelDatas[indexLevel] = levelDataModel;

        OpenNextLevel(indexLevel);

        print($"Update level data {levelDataModel.IsNewLevel}");

        Instance.playerData.Save();
    }

    public static void OpenNextLevel(int currentLevelIndex)
    {
        if (currentLevelIndex + 1 == PlayerDataModel.LevelDatas.Count) return;

        print($"Open Level : {currentLevelIndex + 1}");

        LevelDataModel nextTarget = PlayerDataModel.LevelDatas[currentLevelIndex + 1];

        nextTarget.IsOpen = true;

        PlayerDataModel.LevelDatas[currentLevelIndex + 1] = nextTarget;
    }
}
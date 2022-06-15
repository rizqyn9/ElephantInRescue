using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Dev : MonoBehaviour
{
    #region Singleton
    private static Dev _instance;
    public static Dev Instance { get => _instance; }
    #endregion

    [Header("Properties")]
    public bool isDevMode;
    public bool useCustomUserModel = true;
    public bool useNewDataUser = false;
    public PlayerDataModel m_customPlayerModel;
    public bool useLevelTest = true;
    public GameObject gameManagerPrefab;

    private void OnEnable()
    {
        isDevMode = false;
//#if UNITY_EDITOR
//        isDevMode = true;  
//#endif
    }

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
        {
            GameManager.PlayerDataModel = m_customPlayerModel;
            GameManager.Instance.initialize();

        }
        else
        {
            //LevelManager.Instance.Init();
        }
    }
}


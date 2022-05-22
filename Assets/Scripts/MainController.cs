using UnityEngine;

/**
    * Control any event
    * Dont destroy
    * Run on first init
    */
public class MainController : MonoBehaviour
{
    private static MainController _instance;
    public static MainController Instance { get => _instance; }

    [Header("Debug")]
    public LevelBase levelBase;
    [SerializeField] LevelBase currentLevelBase;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void StartGame()
    {
        print("<color=green>Game Started</color>");
    }
}


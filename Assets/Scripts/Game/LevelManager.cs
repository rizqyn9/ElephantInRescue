using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelConfiguration
{
    public int CountDown;
    public GameObject GOMainComponent;
    public GameObject GOTutorial;
}

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get => _instance; }

    [Header("LevelConfig")]
    [SerializeField] LevelConfiguration m_levelConfiguration;
    [SerializeField] List<GameObject> m_inventoryGO = new List<GameObject>();

    [Header("Event")]
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] UITutorial m_uITutorial;
    [SerializeField] InventorySO m_rootController;
    [SerializeField] InventorySO m_stoneController;

    public GameObject MainComponent { get; private set; }
    public LevelDataModel LevelModel { get; private set; }
    public HeaderUtils HeaderUtils { get; set; }
    public LevelSO LevelData { get; private set; }
    public PlayerController PlayerController { get; set; }

    private void OnEnable()
    {
        gameStateChannel.OnEventRaised += HandleGameStateChange;
        HeaderUtils = FindObjectOfType<HeaderUtils>();
        LevelData = GameManager.LevelSO;

        if (!HeaderUtils)
            throw new System.Exception("Header Utils not found");
    }

    private void OnDisable()
    {
        gameStateChannel.OnEventRaised -= HandleGameStateChange;
    }

    [SerializeField] GameState m_gameState;
    void HandleGameStateChange(GameState before, GameState gameState)
    {
        m_gameState = gameState;

        if (gameState == GameState.PLAY)
            HeaderUtils.CountDown.Start();
        if (gameState == GameState.FINISH)
            HeaderUtils.CountDown.Stop();
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    void Start()
    {
        // Instantiate Main Component
        MainComponent = Instantiate(LevelData.GO_MainComponent, new Vector2(0, 2.6f), Quaternion.identity);
        //MainComponent.SetActive(true);

        // Get Player
        PlayerController = FindObjectOfType<PlayerController>();
        if (!PlayerController) throw new System.Exception("Player not found");

        // Reset all tools
        m_rootController.Set(0);
        m_stoneController.Set(0);

        StartCoroutine(StartEnumerator());
    }

    IEnumerator StartEnumerator()
    {
        gameStateChannel.RaiseEvent(GameState.BEFORE_PLAY); // Reset
        LeanTween
            .moveZ(MainComponent, 0, 1f)
            .setFrom(2)
            .setOnComplete(() =>
            {
                if(m_uITutorial)
                {
                    m_uITutorial.gameObject.SetActive(true);
                } else
                {
                    gameStateChannel.RaiseEvent(GameState.PLAY);
                }
            });

        LeanTween
            .alpha(MainComponent, 1, 1f)
            .setFrom(0);

        yield return 1;
    }

    internal void OnCountDownChange() { }

    internal void OnTimeOut()
    {
    }

    public List<GameObject> GetInventoryGO() => m_inventoryGO;

    public void WinCondition()
    {
        gameStateChannel.RaiseEvent(GameState.FINISH);
    }

    public void LoseCondition()
    {
        gameStateChannel.RaiseEvent(GameState.FINISH);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get => _instance; }

    [Header("LevelConfig")]
    [SerializeField] List<GameObject> m_inventoryGO = new List<GameObject>();

    [Header("Event")]
    [SerializeField] Canvas canvas;
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] InventorySO m_rootController;
    [SerializeField] InventorySO m_stoneController;

    public UITutorial UITutorial { get; set; }
    public GameObject MainComponent { get; private set; }
    public LevelDataModel LevelModel { get; private set; }
    public HeaderUtils HeaderUtils { get; set; }
    public LevelSO LevelData { get; private set; }
    public PlayerController PlayerController { get; set; }
    public GameState GameState { get; private set; }

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

    void HandleGameStateChange(GameState before, GameState gameState)
    {
        GameState = gameState;

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

        gameStateChannel.RaiseEvent(GameState.BEFORE_PLAY); // Reset

        if (LevelData.ShouldTutorialUI)
        {
            UITutorial = Instantiate(LevelData.GO_Tutorial, canvas.transform).GetComponent<UITutorial>();
        }

        InstantiateMainComponent();
    }

    void InstantiateMainComponent()
    {
        LeanTween
            .moveZ(MainComponent, 0, 1f)
            .setFrom(2)
            .setOnComplete(() =>
            {
                if (LevelData.ShouldTutorialUI)
                    UITutorial.gameObject.SetActive(true);
                else
                    gameStateChannel.RaiseEvent(GameState.PLAY);
            });

        LeanTween
            .alpha(MainComponent, 1, 1f)
            .setFrom(0);
    }

    internal void OnCountDownChange() { }

    internal void OnTimeOut()
    {
        LoseCondition();
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
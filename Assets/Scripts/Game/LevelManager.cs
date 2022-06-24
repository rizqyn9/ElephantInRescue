using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelConfiguration
{
    public int CountDown;
}

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get => _instance; }

    [Header("LevelConfig")]
    [SerializeField] LevelConfiguration m_levelConfiguration;
    [SerializeField] List<GameObject> m_inventoryGO = new List<GameObject>();

    [Header("Event")]
    [SerializeField] GameStateChannelSO gameStateChannel = default;
    [SerializeField] UITutorial m_uITutorial;
    [SerializeField] GameObject m_gameContainer;
    [SerializeField] InventorySO m_rootController;
    [SerializeField] InventorySO m_stoneController;

    public LevelDataModel LevelModel { get; private set; }
    public HeaderUtils HeaderUtils { get; set; }
    public LevelConfiguration LevelConfiguration { get => m_levelConfiguration; }

    private void OnEnable()
    {
        gameStateChannel.OnEventRaised += HandleGameStateChange;
        HeaderUtils = FindObjectOfType<HeaderUtils>();

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
        m_rootController.Set(0);
        m_stoneController.Set(0);

        gameStateChannel.RaiseEvent(GameState.BEFORE_PLAY); // Reset
        StartCoroutine(StartEnumerator());
    }

    IEnumerator StartEnumerator()
    {
        LeanTween
            .moveZ(m_gameContainer, 0, 5f)
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
            .alpha(m_gameContainer, 1, 1f)
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
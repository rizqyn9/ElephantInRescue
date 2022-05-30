using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get => _instance; }

    [SerializeField] List<GameObject> m_inventoryGO = new List<GameObject>();

    [Header("Event")]
    [SerializeField] GameStateChannelSO gameStateChannel = default;
    [SerializeField] UITutorial m_uITutorial;
    [SerializeField] GameObject m_gameContainer;

    public LevelDataModel LevelModel { get; private set; }
    public int CountTimeOut = 100;

    private void OnEnable()
    {
        gameStateChannel.OnEventRaised += HandleGameStateChange;
    }

    private void OnDisable()
    {
        gameStateChannel.OnEventRaised -= HandleGameStateChange;
    }

    [SerializeField] GameState m_gameState;
    void HandleGameStateChange(GameState before, GameState gameState)
    {
        m_gameState = gameState;
    }


    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    void Start()
    {
        gameStateChannel.RaiseEvent(GameState.BEFORE_PLAY);
        StartCoroutine(StartEnumerator());
    }

    IEnumerator StartEnumerator()
    {
        LeanTween
            .moveZ(m_gameContainer, 0, 1f)
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


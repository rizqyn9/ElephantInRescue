using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get => _instance; }

    [SerializeField] List<GameObject> m_inventoryGO = new List<GameObject>();

    [Header("Event")]
    [SerializeField] GameStateChannelSO gameStateChannel = default;

    public LevelBase LevelBase { get; private set; }

    private void OnEnable()
    {
        gameStateChannel.OnEventRaised += HandleGameStateChange;
    }

    private void OnDisable()
    {
        gameStateChannel.OnEventRaised -= HandleGameStateChange;
    }

    [SerializeField] GameState m_gameState;
    void HandleGameStateChange(GameState gameState)
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
        // Start the game after all animation have done
        Camera.main.transform.DOMoveZ(Camera.main.transform.position.z, 3).SetEase(Ease.InOutQuart).From(0).OnComplete(() =>
        {
            gameStateChannel.RaiseEvent(GameState.PLAY);
        });

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


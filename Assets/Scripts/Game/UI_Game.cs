using UnityEngine;
using TMPro;

public class UI_Game : MonoBehaviour
{
    private static UI_Game _instance;
    public static UI_Game Instance { get => _instance; }

    [SerializeField] UI_Inventory m_uI_Inventory;
    [SerializeField] GameStateChannelSO gameState;

    private void OnEnable()
    {
        gameState.OnEventRaised += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        gameState.OnEventRaised -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.PLAY:

                break;
        }
    }

    private void Start()
    {
        // Get inverntory data from Level Manager
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    public void Init()
    {
    }

    public void UpdateUI() { }

    public void Btn_Pause()
    {

    }
}

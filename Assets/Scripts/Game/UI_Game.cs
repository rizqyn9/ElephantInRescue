using UnityEngine;

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

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void HandleGameStateChanged(GameState before, GameState gameState)
    {
        switch (gameState)
        {
            case GameState.BEFORE_PLAY:
                break;
            case GameState.PLAY:
                if (Time.timeScale == 0) Play();
                break;
            case GameState.PAUSE:
                if (Time.timeScale == 0) return;
                Pause();
                break;
        }
    }

    void Pause() => Time.timeScale = 0;
    void Play() => Time.timeScale = 1;

    public void Btn_Restart()
    {
        GameManager.LoadGameLevel(LevelManager.Instance.LevelModel);
    }

    public void Btn_BackToMainMenu()
    {
        GameManager.LoadMainMenu();
    }
}

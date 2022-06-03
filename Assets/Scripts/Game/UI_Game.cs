using UnityEngine;

public class UI_Game : MonoBehaviour
{
    private static UI_Game _instance;
    public static UI_Game Instance { get => _instance; }

    [SerializeField] UI_Inventory m_uI_Inventory;
    [SerializeField] GameStateChannelSO gameState;
    [SerializeField] CanvasGroup gameUI;
    [SerializeField] Canvas m_canvas;

    private void OnEnable()
    {
        gameState.OnEventRaised += HandleGameStateChanged;
        gameUI.alpha = 0;
        gameUI.interactable = false;
        m_canvas = GetComponent<Canvas>();

    }

    private void Start()
    {
        m_canvas.worldCamera = Camera.main;
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
                LeanTween
                    .alphaCanvas(gameUI, 1, .3f)
                    .setOnComplete(() => { gameUI.interactable = true; });

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

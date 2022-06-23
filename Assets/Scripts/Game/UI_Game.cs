using UnityEngine;

public class UI_Game : MonoBehaviour
{
    private static UI_Game _instance;
    public static UI_Game Instance { get => _instance; }

    [SerializeField] UI_Inventory m_uI_Inventory;
    [SerializeField] GameStateChannelSO gameState;
    [SerializeField] CanvasGroup gameUI;

    Canvas Canvas { get; set; }
    public LevelManager LevelManager { get; set; }

    private void OnEnable()
    {
        gameState.OnEventRaised += HandleGameStateChanged;

        gameUI.alpha = 0;
        gameUI.interactable = false;

        Canvas = GetComponent<Canvas>();
        LevelManager = LevelManager.Instance;
    }

    private void Start()
    {
        Canvas.worldCamera = Camera.main;
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

    public bool IsPaused() => Time.timeScale == 0;

    public void Btn_Restart()
    {
        GameManager.LoadGameLevel(GameManager.Instance.LevelDataModel);
    }

    public void Btn_NextLevel()
    {
        print("Next Level unhandled now");
    }

    public void Btn_BackToMainMenu()
    {
        GameManager.LoadMainMenu();
    }
}

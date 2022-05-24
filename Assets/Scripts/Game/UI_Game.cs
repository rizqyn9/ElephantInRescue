using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    private static UI_Game _instance;
    public static UI_Game Instance { get => _instance; }

    [SerializeField] UI_Inventory m_uI_Inventory;
    [SerializeField] GameStateChannelSO gameState;
    [SerializeField] GameObject m_winGO, m_loseGO;
    [SerializeField] Text m_countDown;

    private void OnEnable()
    {
        gameState.OnEventRaised += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        gameState.OnEventRaised -= HandleGameStateChanged;
    }

    private void Start()
    {
        
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void HandleGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.BEFORE_PLAY:
                m_countDown.text = (10).ToString();
                StartCoroutine(ICountDown(10));                
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

    IEnumerator ICountDown (int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            m_countDown.text = counter.ToString();
        }
    }

    void Pause() => Time.timeScale = 0;
    void Play() => Time.timeScale = 1;

    public void Btn_Pause()
    {

    }
}

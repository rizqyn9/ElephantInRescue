using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesController : MonoBehaviour
{
    [SerializeField] Image m_stars, m_title, m_emotion, m_levelTitle;
    [SerializeField] GameObject m_winContainer, m_loseContainer;
    [SerializeField] UIDialog m_uIDialog;
    [SerializeField] List<RulesProps> m_rulesProps = new List<RulesProps>();
    [SerializeField] AudioClip m_winSFX, m_loseSFX;

    [SerializeField] GameStateChannelSO m_gameStateChannel;
    [SerializeField] LevelDataModel m_levelDataModel;

    public PlayerController PlayerController { get => PlayerController.Instance; }

    void OnEnable()
    {
        m_gameStateChannel.OnEventRaised += HandleGameStateChanged;
    }

    void OnDisable()
    {
        m_gameStateChannel.OnEventRaised -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState before, GameState gameState)
    {
        switch (gameState)
        {
            case GameState.BEFORE_PLAY:
                m_uIDialog.gameObject.SetActive(false);
                m_levelDataModel = GameManager.Instance.LevelDataModel;
                break;
            case GameState.PLAY:
                break;
            case GameState.FINISH:
                CalculateStar();
                break;
        }
    }

    void CalculateStar()
    {
        int remainingTime = LevelManager.Instance.HeaderUtils.CountDown.GetRemainingTime();

        if (PlayerController.IsDead) HandlePoint(WinLoseType.LOSE);
        else 
            HandlePoint(LevelManager.Instance.LevelData.CalculateStar(remainingTime));
    }


    void HandlePoint(WinLoseType type)
    {
        m_levelDataModel = GameManager.Instance.LevelDataModel;
        RenderUI(m_rulesProps.Find(val => val.WinLoseType == type));

        m_levelDataModel.IsNewLevel = false;    // Update recently play
        m_levelDataModel.IsOpen = true;         // Ensure reached level still open

        switch (type)
        {
            case WinLoseType.TIME_OUT:
            case WinLoseType.LOSE:
                m_levelDataModel.Stars = 0;
                SoundManager.PlaySound(m_loseSFX);
                break;
            case WinLoseType.STARS1:
                m_levelDataModel.Stars = 1;
                SoundManager.PlaySound(m_winSFX);
                break;
            case WinLoseType.STARS2:
                m_levelDataModel.Stars = 2;
                SoundManager.PlaySound(m_winSFX);
                break;
            case WinLoseType.STARS3:
                m_levelDataModel.Stars = 3;
                SoundManager.PlaySound(m_winSFX);
                break;
            default:
                Debug.LogError("Unhandled Win Lose Type");
                break;
        }

        SaveToPersistant();
    }

    void RenderUI(RulesProps props)
    {
        m_levelTitle.sprite = LevelManager.Instance.LevelData.LevelTitle;
        m_title.sprite = props.Title;
        m_stars.sprite = props.Stars;
        m_emotion.sprite = props.Emotion;
        m_winContainer.SetActive(props.IsWin);
        m_loseContainer.SetActive(!props.IsWin);
        m_uIDialog.gameObject.SetActive(true);
    }

    private void SaveToPersistant()
    {
        GameManager.UpdatePlayerData(m_levelDataModel);
    }
}

public enum WinLoseType
{
    TIME_OUT,
    LOSE,
    STARS1,
    STARS2,
    STARS3
}

[System.Serializable]
public struct RulesProps
{
    public WinLoseType WinLoseType;
    public Sprite Title;
    public Sprite Stars;
    public Sprite Emotion;
    public bool IsWin;
}
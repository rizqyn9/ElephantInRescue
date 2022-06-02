using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesController : MonoBehaviour
{
    [SerializeField] Image m_stars, m_title, m_emotion;
    [SerializeField] GameObject m_winContainer, m_loseContainer;
    [SerializeField] UIDialog m_uIDialog;
    [SerializeField] List<RulesProps> m_rulesProps = new List<RulesProps>();

    [SerializeField] GameStateChannelSO m_gameStateChannel;

    [HideInInspector] [SerializeField] PlayerController PlayerController { get => PlayerController.Instance; }

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
            case GameState.FINISH:
                CalculateStar();
                break;
        }
    }

    void CalculateStar()
    {
        float remainingTime = GameObject.FindGameObjectWithTag("CountDown").GetComponent<CountDown>().CountTime;

        if (PlayerController.IsDead) HandlePoint(WinLoseType.LOSE);
        else if (remainingTime >= 12) HandlePoint(WinLoseType.STARS3);
        else if (remainingTime >= 8) HandlePoint(WinLoseType.STARS2);
        else if (remainingTime >= 4) HandlePoint(WinLoseType.STARS1);
        else HandlePoint(WinLoseType.TIME_OUT);
    }

    void HandlePoint(WinLoseType type)
    {
        RenderUI(m_rulesProps.Find(val => val.WinLoseType == type));
    }

    void RenderUI(RulesProps props)
    {
        m_title.sprite = props.Title;
        m_stars.sprite = props.Stars;
        m_emotion.sprite = props.Emotion;
        m_winContainer.SetActive(props.IsWin);
        m_loseContainer.SetActive(!props.IsWin);
        m_uIDialog.gameObject.SetActive(true);
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
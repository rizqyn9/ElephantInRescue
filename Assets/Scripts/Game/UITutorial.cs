using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharExpression
{
    SMILE,
    TALK,
    SAD
}

[System.Serializable]
public struct TutorialProps
{
    [TextArea] public string Text;
    public CharExpression CharExpression;    
}

[System.Serializable]
public struct SpriteExpression
{
    public CharExpression CharExpression;
    public Sprite Sprite;
}

public class UITutorial : MonoBehaviour
{
    [SerializeField] Image m_char;
    [SerializeField] Image m_textContainer;
    [SerializeField] TMPro.TMP_Text m_textScript;
    [SerializeField] List<TutorialProps> m_tutorials;
    [SerializeField] GameStateChannelSO m_gameStateChannel;
    [SerializeField] List<SpriteExpression> SpriteExpressions;

    public CanvasGroup CanvasGroup { get; private set; }

    private void OnEnable()
    {
        m_gameStateChannel.RaiseEvent(GameState.TUTORIAL);

        CanvasGroup = GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        m_char.enabled = false;

        m_textScript.text = "";

        LeanTween
            .alphaCanvas(CanvasGroup, 1, .8f)
            .setOnComplete(() => {
                CanvasGroup.interactable = true;
                StartText();
            });
    }

    int indexTextCounter;
    void StartText()
    {
        indexTextCounter = 0;
        StartCoroutine(PlayText(m_tutorials[indexTextCounter]));
    }

    bool shouldNext = false;
    bool shouldSkip = false;
    IEnumerator PlayText(TutorialProps props)
    {
        m_textScript.text = "";

        SetCharSprite(GetSprite(props.CharExpression));

        shouldNext = false;
        shouldSkip = false;

        foreach (char c in props.Text)
        {
            m_textScript.text += c;
            if(!shouldSkip)
                yield return new WaitForSeconds(0.1f);
        }

        shouldNext = true;
    }

    public void Btn_OnNext()
    {
        if (!shouldNext) shouldSkip = true;
        else if (shouldNext && indexTextCounter < m_tutorials.Count - 1)
        {
            indexTextCounter++;
            StartCoroutine(PlayText(m_tutorials[indexTextCounter]));
        } else if (shouldNext && indexTextCounter == m_tutorials.Count - 1)
        {
            CloseContainer();
        }
    }

    void CloseContainer ()
    {
        LeanTween
            .alphaCanvas(CanvasGroup, 0, .4f)
            .setOnComplete(() =>
            {
                m_gameStateChannel.RaiseEvent(GameState.PLAY);
                gameObject.SetActive(false);
            });

        LeanTween
            .moveY(gameObject.GetComponent<RectTransform>(), -100, .4f)
            .setEaseOutQuad();
    }

    Sprite GetSprite(CharExpression mode) =>
        SpriteExpressions.Find(val => val.CharExpression == mode).Sprite;

    void SetCharSprite(Sprite sprite)
    {
        if (!sprite) m_char.enabled = false;
        else
        {
            m_char.sprite = sprite;
            m_char.enabled = true;
        }
    }
}

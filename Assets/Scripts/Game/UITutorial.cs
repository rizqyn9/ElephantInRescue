using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] Image m_char;
    [SerializeField] Image m_textContainer;
    [SerializeField] TMP_Text m_textScript;
    [HideInInspector] CanvasGroup m_canvasGroup;
    [SerializeField][TextArea] string[] m_texts;
    [SerializeField] GameStateChannelSO m_gameStateChannel;

    private void OnEnable()
    {
        m_gameStateChannel.RaiseEvent(GameState.TUTORIAL);

        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvasGroup.alpha = 0;
        m_canvasGroup.interactable = false;

        m_textScript.text = "";

        LeanTween
            .alphaCanvas(m_canvasGroup, 1, .8f)
            .setOnComplete(() => {
                m_canvasGroup.interactable = true;
                StartText();
            });
    }

    int indexTextCounter;
    void StartText()
    {
        indexTextCounter = 0;
        StartCoroutine(PlayText(m_texts[indexTextCounter]));
    }

    bool shouldNext = false;
    bool shouldSkip = false;
    IEnumerator PlayText(string text)
    {
        m_textScript.text = "";

        shouldNext = false;
        shouldSkip = false;

        foreach (char c in text)
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
        else if (shouldNext && indexTextCounter < m_texts.Length - 1)
        {
            indexTextCounter++;
            StartCoroutine(PlayText(m_texts[indexTextCounter]));
        } else if (shouldNext && indexTextCounter == m_texts.Length - 1)
        {
            CloseContainer();
        }
    }

    void CloseContainer ()
    {
        LeanTween
            .alphaCanvas(m_canvasGroup, 0, .4f)
            .setOnComplete(() =>
            {
                m_gameStateChannel.RaiseEvent(GameState.PLAY);
                gameObject.SetActive(false);
            });

        LeanTween
            .moveY(gameObject.GetComponent<RectTransform>(), -100, .4f)
            .setEaseOutQuad();
    }
}

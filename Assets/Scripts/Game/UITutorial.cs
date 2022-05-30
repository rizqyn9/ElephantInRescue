using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] Image m_char;
    [SerializeField] Image m_textContainer;
    [SerializeField] TMP_Text m_textScript;
    [HideInInspector] CanvasGroup m_canvasGroup;

    private void OnEnable()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvasGroup.alpha = 0;

        LeanTween
            .alphaCanvas(m_canvasGroup, 1, 2f);
    }

}

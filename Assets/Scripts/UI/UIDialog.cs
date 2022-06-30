using System;
using UnityEngine;
using UnityEngine.Events;

public class UIDialog : MonoBehaviour
{
    [SerializeField] RectTransform m_child;
    [SerializeField] UnityAction m_onClose;
    internal Action CloseCallback;

    public UI_MainMenu UI_MainMenu { get; set; }

    private void OnEnable()
    {
        UI_MainMenu = GetComponentInParent<UI_MainMenu>();
        m_child.GetComponent<CanvasGroup>().alpha = 0;

        LeanTween
            .alphaCanvas(m_child.GetComponent<CanvasGroup>(), 1, .5f)
            .setIgnoreTimeScale(true);

        LeanTween
            .moveY(m_child, 0, .5f).setFrom(m_child.rect.y - 200)
            .setEaseInOutCirc()
            .setIgnoreTimeScale(true);
    }

    void Close()
    {
        m_onClose?.Invoke();

        UI_MainMenu.ButtonClick();

        LeanTween
            .alphaCanvas(m_child.GetComponent<CanvasGroup>(), 0, 1f)
            .setIgnoreTimeScale(true);

        LeanTween
            .moveY(m_child, m_child.rect.y - 300, .5f)
            .setOnComplete(() => {
                CloseCallback?.Invoke();
                gameObject.SetActive(false);
            })
            .setIgnoreTimeScale(true);
    }

    public void OnClose()
    {
        Close();
    }
}

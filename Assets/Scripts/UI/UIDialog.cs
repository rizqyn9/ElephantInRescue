using UnityEngine;
using UnityEngine.Events;

public class UIDialog : MonoBehaviour
{
    [SerializeField] RectTransform m_child;
    [SerializeField] UnityAction m_onClose;
    [SerializeField] VoidEventChannelSO m_buttonClickSFX;

    internal System.Action CloseCallback;

    public CanvasGroup CanvasGroup { get; set; }

    private void OnEnable()
    {
        CanvasGroup = m_child.GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;

        LeanTween
            .alphaCanvas(m_child.GetComponent<CanvasGroup>(), 1, .5f)
            .setIgnoreTimeScale(true);

        LeanTween
            .moveY(m_child, 0, .5f).setFrom(m_child.rect.y - 200)
            .setEaseInOutCirc()
            .setIgnoreTimeScale(true)
            .setOnComplete(() => { CanvasGroup.interactable = true; });
    }

    void Close()
    {
        m_onClose?.Invoke();

        m_buttonClickSFX.RaiseEvent();

        CanvasGroup.interactable = false;

        LeanTween
            .alphaCanvas(m_child.GetComponent<CanvasGroup>(), 0, .2f)
            .setIgnoreTimeScale(true);

        LeanTween
            .moveY(m_child, m_child.rect.y - 300, .25f)
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

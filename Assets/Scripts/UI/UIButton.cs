using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO m_buttonClick;
    [SerializeField] UIDialog m_target;
    [SerializeField] UnityEvent onClick;

    public Button Button { get; private set; }
    public UIDialog UIDialog { get => m_target; }

    private void OnEnable()
    {
        Button = gameObject.GetComponent<Button>();
        Button.onClick.AddListener(On_Click);
    }

    public void On_Click()
    {
        RectTransform rect = Button.GetComponent<RectTransform>();
        m_buttonClick.RaiseEvent();
        LeanTween
            .scale(rect, rect.localScale * 1.1f, .2f)
            .setLoopPingPong(1)
            .setEaseInBounce()
            .setIgnoreTimeScale(true)
            .setOnComplete(() => {
                if (m_target)
                    m_target.gameObject.SetActive(true);
                onClick?.Invoke();
                rect.localScale = Vector3.one;
            });
    }

    public void OnClose()
    {
        m_target.OnClose();
    }
}


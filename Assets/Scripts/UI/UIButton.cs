using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] UIDialog m_target;
    [SerializeField] UnityEvent onClick;
    Button m_button;

    public UIDialog UIDialog { get => m_target; }

    private void OnEnable()
    {
        m_button = gameObject.GetComponent<Button>();
        m_button.onClick.AddListener(On_Click);
    }

    public void On_Click()
    {
        RectTransform rect = m_button.GetComponent<RectTransform>();
        LeanTween
            .scale(rect, rect.localScale * 1.1f, .2f)
            .setLoopPingPong(1)
            .setEaseInBounce()
            .setIgnoreTimeScale(true)
            .setOnComplete(() => {
                if (m_target)
                    m_target.gameObject.SetActive(true);
                onClick?.Invoke();
            });
    }

    public void OnClose()
    {
        m_target.OnClose();
    }
}

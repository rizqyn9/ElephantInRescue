using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] UIDialog m_target;
    [SerializeField] UnityEvent onClick;
    [HideInInspector] Button m_button;

    private void OnEnable()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(On_Click);
    }

    public void On_Click()
    {
        RectTransform rect = m_button.GetComponent<RectTransform>();
        LeanTween
            .scale(rect, rect.localScale * 1.1f, .2f)
            .setLoopPingPong(1)
            .setEaseInBounce()
            .setOnComplete(() => {
                if (m_target)
                    m_target.gameObject.SetActive(true);
                else
                    onClick?.Invoke();
            });
    }

    public void OnClose()
    {
        m_target.OnClose();
    }
}

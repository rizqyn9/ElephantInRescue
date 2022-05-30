using UnityEngine;

public class UINotes : MonoBehaviour
{
    [SerializeField] UI_MainMenu m_Ui_MainMenu;
    [SerializeField] GameObject m_primaryContainer;
    [SerializeField] GameObject m_containerState;
    [SerializeField] UIDialog m_dialog;

    private GameObject ContainerState
    {
        get => m_containerState;
        set
        {
            if (m_containerState == value) return;
            HandleOnContainerChanged(m_containerState, value);
            m_containerState = value;
        }
    }

    private void OnEnable()
    {
        ContainerState = m_primaryContainer;
        m_dialog = GetComponent<UIDialog>();
    }

    public void OnChange(GameObject _containerOpen)
    {
        ContainerState = _containerOpen;
    }

    public void HandleOnContainerChanged(GameObject _before, GameObject _after)
    {
        if (_before != null) _before.SetActive(false);
        _after.SetActive(true);
    }

    public void On_BackContainer()
    {
        if (m_primaryContainer.name == m_containerState.name) m_dialog.OnClose();
        else OnChange(m_primaryContainer);
    }
}

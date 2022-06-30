using UnityEngine;

public class UINotes : MonoBehaviour
{
    [SerializeField] GameObject m_primaryContainer;
    [SerializeField] GameObject m_containerState;
    [SerializeField] UIDialog m_dialog;

    public UI_MainMenu m_uiMainMenu { get; private set; }

    public GameObject ContainerState
    {
        get => m_containerState;
        private set
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
        m_uiMainMenu = GetComponentInParent<UI_MainMenu>();
    }

    public void OnChange(GameObject _containerOpen)
    {
        m_uiMainMenu.ButtonClick();
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

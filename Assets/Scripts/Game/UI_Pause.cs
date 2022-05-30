using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UI_Pause : MonoBehaviour
{
    [SerializeField] GameStateChannelSO m_GameStateChannelSO;

    UIButton m_uIButton;

    private void OnEnable()
    {
        m_uIButton = GetComponent<UIButton>();
        m_uIButton.UIDialog.CloseCallback += Btn_Cancel;
    }

    private void OnDisable()
    {
        m_uIButton.UIDialog.CloseCallback -= Btn_Cancel;        
    }

    public void Btn_Pause()
    {
        m_GameStateChannelSO.RaiseEvent(GameState.PAUSE);
        m_uIButton.UIDialog.gameObject.SetActive(true);
    }

    public void Btn_Cancel()
    {
        m_GameStateChannelSO.RaiseEvent(GameState.PLAY);
    }

    public void Btn_Resume() => Btn_Cancel();

    public void Btn_MainMenu()
    {
        GameManager.LoadMainMenu();
    }

    public void Btn_Restart()
    {
        GameManager.LoadGameLevel(new LevelDataModel());
    }
}

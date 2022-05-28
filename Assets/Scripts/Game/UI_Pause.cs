using UnityEngine;
using UnityEngine.UI;
using EIR;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] Button pauseBtn, cancelBtn;
    [SerializeField] GameObject modalPause;

    [SerializeField] GameStateChannelSO m_GameStateChannelSO;

    public void test(bool a)
    {
        print(a);
    }

    public void Btn_Pause()
    {
        m_GameStateChannelSO.RaiseEvent(GameState.PAUSE);
        if (modalPause.activeSelf) return;

        modalPause.SetActive(true);
    }

    public void Btn_Cancel()
    {
        if (!modalPause.activeSelf) return;
        m_GameStateChannelSO.RaiseEvent(GameState.PLAY);
        modalPause.SetActive(false);
    }

    public void Btn_Resume()
    {
        Btn_Cancel();
    }

    public void Btn_MainMenu()
    {
        GameManager.LoadMainMenu();
    }

    public void Btn_Restart()
    {
        GameManager.LoadGameLevel(new LevelBase());
    }
}

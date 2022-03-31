using UnityEngine;
using UnityEngine.UI;
using EIR;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] Button pauseBtn, cancelBtn;
    [SerializeField] GameObject modalPause;

    public bool isPaused { get; private set; }

    public void Btn_Pause()
    {
        if (modalPause.activeSelf) return;

        modalPause.SetActive(true);
    }

    public void Btn_Cancel()
    {
        if (!modalPause.activeSelf) return;
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
}

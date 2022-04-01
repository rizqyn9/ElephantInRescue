using UnityEngine;
using UnityEngine.UI;
using EIR;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] Button pauseBtn, cancelBtn;
    [SerializeField] GameObject modalPause;

    [SerializeField] BoolEventChannelSO testBool;

    public bool isPaused { get; private set; }

    private void OnEnable()
    {
        testBool.OnEventRaised += test;
    }

    private void OnDisable()
    {
        testBool.OnEventRaised -= test;
    }

    public void test(bool a)
    {
        print(a);
    }

    public void Btn_Pause()
    {
        testBool.RaiseEvent(true);
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

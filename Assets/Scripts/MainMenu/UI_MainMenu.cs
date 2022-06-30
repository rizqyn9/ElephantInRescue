using UnityEngine;
using TMPro;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO m_buttonClickSO;
    #region ButtonHandler

    public void Btn_Play()
    {
        ButtonClick();
        MainMenuManager.Instance.Play();
    }

    public void CloseApplication()
    {
        GameManager.CloseApplication();
    }

    public void ButtonClick()
    {
        m_buttonClickSO.RaiseEvent();
    }

    #endregion
}


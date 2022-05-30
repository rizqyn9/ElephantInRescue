using UnityEngine;
using TMPro;

public class UI_MainMenu : MonoBehaviour
{
    #region ButtonHandler

    public void Btn_Play()
    {
        MainMenuManager.Instance.Play();
    }

    public void CloseApplication()
    {
        GameManager.CloseApplication();
    }

    #endregion
}


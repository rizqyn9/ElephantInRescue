using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}

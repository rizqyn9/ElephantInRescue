using System.Collections.Generic;
using UnityEngine;

public class UIMMTutorial : MonoBehaviour
{
    [SerializeField] List<GameObject> pages = new List<GameObject>();
    GameObject m_pageActive;
    int m_indexActive = 0;

    public UI_MainMenu UI_MainMenu { get; private set; }

    public int IndexActive
    {
        get => m_indexActive;
        set
        {
            pages.ForEach(val => val.SetActive(false));
            pages[value].SetActive(true);
            m_indexActive = value;
        }
    }

    private void OnEnable()
    {
        UI_MainMenu = GetComponentInParent<UI_MainMenu>();
        IndexActive = 0;   
    }

    public void Btn_Next()
    {
        if (m_indexActive == pages.Count - 1) IndexActive = 0;
        else IndexActive++;
        UI_MainMenu.ButtonClick();
    }

    public void Btn_Prev()
    {
        if (m_indexActive == 0) IndexActive = pages.Count - 1;
        else IndexActive--;
        UI_MainMenu.ButtonClick();
    }
}

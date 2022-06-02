using UnityEngine;

public class UILevel : MonoBehaviour
{
    [SerializeField] UIDialog m_uIDialog;
    [HideInInspector] [SerializeField] LevelItem m_levelItem;

    public LevelItem LevelItem { get => m_levelItem; private set => m_levelItem = value; }

    public void OpenDialog(LevelItem levelItem)
    {
        LevelItem = levelItem;
        m_uIDialog.gameObject.SetActive(true);
    }

    public void PlayLevel()
    {
        if (LevelItem == null) return;
        GameManager.LoadGameLevel(LevelItem.LevelDataModel);
    }

    public void Btn_BackToMainMenu()
    {
        GameManager.LoadMainMenu();
    }
}

using UnityEngine;

public class UILevel : MonoBehaviour
{
    [SerializeField] UIDialog m_uIDialog;

    public LevelItem LevelItem { get; private set; }
    public LevelSO LevelSO { get; private set; }

    public void OpenDialog(LevelItem levelItem)
    {
        LevelItem = levelItem;
        LevelSO = levelItem.LevelSO;
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

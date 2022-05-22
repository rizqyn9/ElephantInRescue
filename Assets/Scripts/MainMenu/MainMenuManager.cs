using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager _instance;
    public static MainMenuManager Instance { get => _instance; }

    [Header("Properties")]
    public UI_MainMenu ui_MainMenu;

    // Accessor
    public static UI_MainMenu UI_MainMenu => Instance.ui_MainMenu;

    private void Awake()
    {
        if (_instance == null)   // Singleton
            _instance = this;
    }

    public void Init()
    {

    }

    public void formSuccess()
    {
        ui_MainMenu.ui_Form.gameObject.SetActive(false);
        ui_MainMenu.UpdateUserName();
        GameManager.PlayerData.Save();
    }

    public void Play()
    {
        //GameManager.LoadGameLevel(ResourcesManager.LevelBase.Find(val => val.level == GameManager.Instance.playerDataModel.currentLevel));
        GameManager.LoadLevelMap();
    }
}


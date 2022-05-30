using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager _instance;
    public static MainMenuManager Instance { get => _instance; }

    [Header("Properties")]
    public UI_MainMenu ui_MainMenu;

    private void Awake()
    {
        if (_instance == null)   // Singleton
            _instance = this;
    }

    public void Init()
    {

    }

    public void Play()
    {
        //GameManager.LoadGameLevel(ResourcesManager.LevelBase.Find(val => val.level == GameManager.Instance.playerDataModel.currentLevel));
        print("Main");
        GameManager.LoadLevelMap();
    }
}


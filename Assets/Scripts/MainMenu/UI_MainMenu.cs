using UnityEngine;
using TMPro;

public class UI_MainMenu : MonoBehaviour
{
    [Header("Properties")]
    public UI_Form ui_Form;
    public TMP_Text profileUserName, levelLevel, levelStage;

    private void Start()
    {
        ui_Form.gameObject.SetActive(false);
        LeanTween.reset();
    }

    public void FormSetActive(bool value) =>
        ui_Form.gameObject.SetActive(value);

    public void UpdateUIComponent()
    {
        UpdateUserName();
        UpdateLevelContainer();
    }

    public void UpdateUserName() => profileUserName.text = GameManager.Instance.playerDataModel.userName;

    public void UpdateLevelContainer()
    {
        levelLevel.text = $"Level {GameManager.Instance.playerDataModel.currentLevel}";
        levelStage.text = $"Stage {GameManager.Instance.playerDataModel.currentStage}";
    }

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


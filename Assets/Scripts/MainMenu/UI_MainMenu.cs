using UnityEngine;
using TMPro;
using System;

public class UI_MainMenu : MonoBehaviour
{
    [Header("Properties")]
    public UI_Form ui_Form;
    public TMP_Text profileUserName, levelLevel, levelStage;
    [SerializeField] RectTransform btnPlay, btnSetting, btnExit, btnNotes, btnTutor;
    [SerializeField] UISetting uISetting;
    [SerializeField] GameObject goOverlay, goOverlayActive;
    [SerializeField] GameObject goTutorial, goNotes;

    private void Start()
    {
        ui_Form.gameObject.SetActive(false);
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
        print("Play");
        EffectOnClick(btnPlay, MainMenuManager.Instance.Play);
        //MainMenuManager.Instance.Play();
    }

    public void Btn_Setting()
    {
        EffectOnClick(btnSetting, () =>
            {
                ToogleWithOverlay(uISetting.gameObject, uISetting.gameObject.SetActive);
                popDialog(uISetting.gameObject);
            });
    }

    public void Btn_Tutorial()
    {
        EffectOnClick(btnTutor, () =>
        {
            ToogleWithOverlay(goTutorial, goTutorial.SetActive);
            popDialog(goTutorial);
        });
    }

    public void Btn_Notes()
    {
        EffectOnClick(btnNotes, () =>
        {
            ToogleWithOverlay(goNotes, goNotes.SetActive);
            popDialog(goNotes);
        });
    }

    #endregion

    public void OnClick_Overlay()
    {
        if (goOverlayActive.activeInHierarchy) goOverlayActive.SetActive(false);
        goOverlay.SetActive(false);
    }

    void ToogleWithOverlay(GameObject _target, Action<bool> _action)
    {
        bool _isActive = _target.activeInHierarchy;
        goOverlayActive = _isActive ? null : _target;
        goOverlay.SetActive(!_isActive);
        _action(!_isActive);
    }

    void popDialog(GameObject _dialog)
    {
        LeanTween.alpha(_dialog, 1, 3f).setFrom(0);
        LeanTween.moveLocalY(_dialog, 0, .4f).setFrom(-500).setEaseOutBounce();
    }

    public void EffectOnClick(RectTransform _target, Action _onComplete)
    {
        Debug.Log("Anim");
        //LeanTween.scale(_target, new Vector3(1.1f, 1.1f, 1), .1f).setEaseInOutCirc().setLoopPingPong(2).setOnStart(() => { Debug.Log("Satrt"); }).setOnComplete(() =>
        //{
        //    _onComplete();
        //});
        _onComplete();
    }
}


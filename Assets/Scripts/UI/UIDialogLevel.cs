using UnityEngine;
using UnityEngine.UI;

public class UIDialogLevel : MonoBehaviour
{
    [SerializeField] UILevel m_uILevel;
    [SerializeField] Image m_stars;
    [SerializeField] TMPro.TMP_Text m_tStar1, m_tStar2, m_tStar3, m_LevelText, m_StageText;

    public LevelItem LevelItem { get; private set; }
    public LevelSO LevelSO { get; set; }
    public UIDialog UIDialog { get; set; }

    private void OnEnable()
    {
        LevelItem = m_uILevel.LevelItem;
        LevelSO = LevelItem.LevelSO;

        if (LevelSO.IsComingSoon)
        {
            throw new System.Exception("Coming soon");
        }

        UIDialog = GetComponent<UIDialog>();
        m_LevelText.text = $"LEVEL {LevelSO.Level}";
        m_StageText.text = $"STAGE {LevelSO.Stage}";
        m_stars.sprite = LevelItem.m_uiStars[LevelItem.LevelDataModel.Stars];
        m_tStar1.text = GetTargetStar(LevelSO.Star1);
        m_tStar2.text = GetTargetStar(LevelSO.Star2);
        m_tStar3.text = GetTargetStar(LevelSO.Star3);
    }

    private void OnDisable()
    {
        LevelItem = null;
        LevelSO = null;
    }

    public void Btn_Play()
    {
        m_uILevel.PlayLevel();
    }

    public void Btn_Back()
    {
        UIDialog.OnClose();
    }

    string GetTargetStar(int target) => $"{target}\"";
}

using UnityEngine;
using UnityEngine.UI;

public class UIDialogLevel : MonoBehaviour
{
    [SerializeField] UILevel m_uILevel;
    [SerializeField] LevelItem m_levelItem = null;
    [SerializeField] Image m_stageTitle, m_stars;

    private void OnEnable()
    {
        m_levelItem = m_uILevel.LevelItem;
        m_stageTitle.sprite = m_levelItem.m_stageSprite;
        m_stars.sprite = m_levelItem.m_uiStars[m_levelItem.LevelDataModel.Stars];
    }

    private void OnDisable()
    {
        m_levelItem = null;
    }

    public void Btn_Play()
    {
        m_uILevel.PlayLevel();
    }
}

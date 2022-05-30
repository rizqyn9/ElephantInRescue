using UnityEngine;
using UnityEngine.UI;

public struct LevelItemProps
{
    public int level;
    public int stage;
    public bool isOpen;
    public int stars;
}

public class LevelItem : MonoBehaviour
{
    [SerializeField] LevelDataModel m_levelData;
    [SerializeField] int m_level, m_stage;
    [SerializeField] Sprite m_uiActive, m_uiDisable;
    [SerializeField] Image m_image, m_stars;
    [SerializeField] Sprite[] m_uiStars;

    private void Start()
    {
        m_levelData = GameManager.GetLevelDataByLevelStage(m_level, m_stage);
        if (m_levelData.IsOpen && m_levelData.IsNewLevel)
        {
            m_image.sprite = m_uiActive;
            m_stars.gameObject.SetActive(false);
        }
        else if (m_levelData.IsOpen && !m_levelData.IsNewLevel)
        {
            m_stars.sprite = m_uiStars[m_levelData.Stars];
            m_stars.gameObject.SetActive(true);
            m_image.sprite = m_uiActive;
        }
        else
        {
            m_stars.gameObject.SetActive(false);
            m_image.sprite = m_uiDisable;
        }
    }

    public void On_SelectLevel()
    {
        print($"Select Stage: {m_stage}, Level: {m_level}");
        // HardCode
        GameManager.LoadGameLevel(m_levelData);
    }
}

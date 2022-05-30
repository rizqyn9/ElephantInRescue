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
    [SerializeField] int m_level, m_stage;
    [Range(0, 3)] [SerializeField] int m_levelStarsTotal = 0;
    [SerializeField] bool m_isOpen = true;
    [SerializeField] bool m_isNewLevel = true;
    [SerializeField] Sprite m_uiActive, m_uiDisable;
    [SerializeField] Image m_image, m_stars;
    [SerializeField] Sprite[] m_uiStars;

    private void Start()
    {
        if (m_isOpen && m_isNewLevel)
        {
            m_image.sprite = m_uiActive;
            m_stars.gameObject.SetActive(false);
        }
        else if (m_isOpen && !m_isNewLevel)
        {
            m_stars.sprite = m_uiStars[m_levelStarsTotal];
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
        GameManager.LoadGameLevel(ResourcesManager.LevelBase.Find(val => val.level == GameManager.Instance.playerDataModel.currentLevel));
    }
}

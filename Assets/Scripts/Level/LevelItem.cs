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
    [SerializeField] Sprite m_uiDisable;
    [SerializeField] Image m_image, m_stars;
    [SerializeField] internal Sprite[] m_uiStars;
    [SerializeField] internal Sprite m_stageSprite;

    [HideInInspector] UILevel m_uILevel;
    [HideInInspector] Button m_button;

    public LevelDataModel LevelDataModel { get => m_levelData; }
    public Image StarsImage { get => m_stars; }

    private void OnEnable()
    {
        m_button = GetComponent<Button>();
    }

    private void Start()
    {
        m_uILevel = FindObjectOfType<UILevel>();

        m_levelData = GameManager.GetLevelDataByLevelStage(m_level, m_stage);
        if (m_levelData.IsOpen && m_levelData.IsNewLevel)
        {
            m_stars.gameObject.SetActive(false);
        }
        else if (m_levelData.IsOpen && !m_levelData.IsNewLevel)
        {
            m_stars.sprite = m_uiStars[m_levelData.Stars];
            m_stars.gameObject.SetActive(true);
        }
        else
        {
            m_button.interactable = false;
            m_stars.gameObject.SetActive(false);
            m_image.sprite = m_uiDisable;
        }
    }

    public void On_SelectLevel()
    {
        print($"Select Stage: {m_stage}, Level: {m_level}");
        m_uILevel.OpenDialog(this);
    }
}

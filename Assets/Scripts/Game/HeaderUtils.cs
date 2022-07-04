using UnityEngine;

public class HeaderUtils : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text m_countText, m_stageText, m_levelText;
    [SerializeField] SpriteRenderer m_elephantHeader;

    public CountDown CountDown { get; set; }
    public LevelManager LevelManager { get; set; }

    private void OnEnable()
    {
        m_countText.text = (0).ToString();
    }

    private void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();        
        CountDown = new CountDown(this);
        Initialize();
    }

    public void Initialize()
    {
        m_elephantHeader.color = LevelManager.LevelData.ElephantColor;
        m_countText.text = LevelManager.LevelData.CountDown.ToString();
        m_stageText.text = $"STAGE {LevelManager.LevelData.Stage}";
        m_levelText.text = $"Level {LevelManager.LevelData.Level}";
    }

    internal void OnCountDownChange()
    {
        m_countText.text = CountDown.CurrentTime.ToString();
    }

    internal void OnTimeOut()
    {
        LevelManager.OnTimeOut();
    }
}

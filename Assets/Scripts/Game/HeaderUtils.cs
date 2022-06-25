using UnityEngine;

public class HeaderUtils : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text m_countText;
    [SerializeField] SpriteRenderer m_titleRenderer;

    public CountDown CountDown { get; set; }
    public LevelManager LevelManager { get; set; }

    private void OnEnable()
    {
        m_countText.text = (0).ToString();
        m_titleRenderer.enabled = false;
    }

    private void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();        
        CountDown = new CountDown(this);
        Initialize();
    }

    public void Initialize()
    {
        m_countText.text = LevelManager.LevelData.CountDown.ToString();
        m_titleRenderer.sprite = LevelManager.LevelData.LevelTitle;
        m_titleRenderer.enabled = true;
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

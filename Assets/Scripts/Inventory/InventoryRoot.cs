using UnityEngine;
using UnityEngine.UI;

public class InventoryRoot : InventoryItem
{
    [SerializeField] int m_count = 0;
    [SerializeField] BoolEventChannelSO m_decreaseIncreaseSO;
    [SerializeField] Text m_text;

    public int Count
    {
        get => m_count;
        private set
        {
            OnChange();
            m_count = value;
        }
    }

    private void Start()
    {
        OnChange();
    }

    private void OnEnable()
    {
        m_decreaseIncreaseSO.OnEventRaised += HandleDecreaseIncrease;
    }

    private void OnDisable()
    {
        m_decreaseIncreaseSO.OnEventRaised -= HandleDecreaseIncrease;
    }

    private void HandleDecreaseIncrease(bool value)
    {
        if (value) m_count++;
        else m_count--;
        OnChange();
    }

    public override void StartConfiguration()
    {
        InventoryItemType = InventoryItemType.ROOT;
    }

    void OnChange()
    {
        m_text.text = m_count.ToString();
    }
}

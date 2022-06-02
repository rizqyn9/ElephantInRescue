using UnityEngine;

public class InventoryStone : InventoryItem
{
    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] BoolEventChannelSO m_addRemoveStone;
    [SerializeField] int m_countStone = 0;

    public int Count
    {
        get => m_countStone;
        internal set
        {
            m_countStone = value;
            if (m_countStone == 0)
                InventoryState = InventoryState.DISABLE;
            if (m_countStone > 0 && InventoryState == InventoryState.DISABLE)
                InventoryState = InventoryState.IDDLE;
            text.text = m_countStone.ToString();
        }
    }

    internal override void OnEnable()
    {
        base.OnEnable();
        m_addRemoveStone.OnEventRaised += HandleAddRemoveStone;

        Count = 0;
    }

    internal override void OnDisable()
    {
        base.OnDisable();
        m_addRemoveStone.OnEventRaised -= HandleAddRemoveStone;
    }

    private void HandleAddRemoveStone(bool shouldAdd)
    {
        if (shouldAdd) Count++;
        else Count--;
    }
}

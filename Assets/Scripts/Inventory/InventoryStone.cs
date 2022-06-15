using System;
using UnityEngine;

public class InventoryStone : InventoryItem
{
    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] InventorySO m_StoneController;
    private int m_countStone = 0;

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
        m_StoneController.OnEventRaised += HandleInventoryCountChange;

        Count = 0;
    }

    internal override void OnDisable()
    {
        base.OnDisable();
        m_StoneController.OnEventRaised -= HandleInventoryCountChange;
    }


    private void HandleInventoryCountChange(int last, int update)
    {
        Count = update;
    }
}

using UnityEngine;
using TMPro;

public class InventoryRoot : InventoryItem
{
    [SerializeField] TMP_Text text;
    [SerializeField] BoolEventChannelSO m_addRemoveRoot;
    [SerializeField] int m_countRoot = 0;
    public int Count
    {
        get => m_countRoot;
        internal set
        {
            m_countRoot = value;
            if (m_countRoot == 0)
                InventoryState = InventoryState.DISABLE;
            if(m_countRoot > 0 && InventoryState == InventoryState.DISABLE) 
                InventoryState = InventoryState.IDDLE;
            text.text = m_countRoot.ToString();
        }
    }

    internal override void OnEnable()
    {
        base.OnEnable();
        m_addRemoveRoot.OnEventRaised += HandleAddRemoveRoot;

        Count = 0;

    }

    internal override void OnDisable()
    {
        base.OnDisable();
        m_addRemoveRoot.OnEventRaised -= HandleAddRemoveRoot;
    }

    private void HandleAddRemoveRoot(bool shouldAdd)
    {
        if (shouldAdd) Count++;
        else Count--;
    }
}

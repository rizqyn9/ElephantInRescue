using UnityEngine;
using TMPro;

public class InventoryRoot : InventoryItem
{
    [SerializeField] TMP_Text text;
    [SerializeField] InventorySO m_rootController;
    private int m_countRoot = 0;
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
        m_rootController.OnEventRaised += HandleRootOnChange;

        Count = 0;

    }

    internal override void OnDisable()
    {
        base.OnDisable();
        m_rootController.OnEventRaised -= HandleRootOnChange;
    }

    private void HandleRootOnChange(int arg0, int arg1)
    {
        Count = arg1;
    }

}

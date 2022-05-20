using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum InventoryItemType
{
    STONE,
    ROOT,
    KNIFE,
    HAMMER
}

[RequireComponent(typeof(Button))]
public class InventoryItem : MonoBehaviour
{
    public InventoryItemType InventoryItemType;
    [SerializeField] bool m_isActive = false;

    [SerializeField] InventoryStateSO m_inventoryChannel;
    [SerializeField] TouchStateSO _touchChannel;

    public bool IsActive { get => m_isActive; set => m_isActive = value; }

    private void OnEnable()
    {
        m_inventoryChannel.OnEventRaised += HandleInventoryChanged;
    }

    private void OnDisable()
    {
        m_inventoryChannel.OnEventRaised -= HandleInventoryChanged;        
    }

    private void Start()
    {
        m_isActive = false;
    }

    private void HandleInventoryChanged(InventoryItem activeInventory)
    {
        if(InventoryItemType != activeInventory.InventoryItemType)
        {
            DisableActivated();
        } else
        {
            EnableActivated();
        }
    }

    private void DisableActivated()
    {
        m_isActive = false;
    }

    private void EnableActivated()
    {
        m_isActive = true;
    }

    public void On_Click()
    {
        if (m_inventoryChannel.ActiveInventory?.InventoryItemType == InventoryItemType) return;
        m_inventoryChannel.RaiseEvent(this);
    }
}

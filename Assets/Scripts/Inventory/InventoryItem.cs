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

public enum InventoryState
{
    ACTIVE,
    IDDLE,
    DISABLE
}

[RequireComponent(typeof(Button))]
public class InventoryItem : MonoBehaviour
{
    [SerializeField] InventoryItemType m_inventoryItemType;

    [SerializeField] InventoryStateSO m_inventoryChannel;
    [SerializeField] TouchStateSO _touchChannel;

    public InventoryItemType InventoryItemType { get => m_inventoryItemType; internal set => m_inventoryItemType = value; }
    public InventoryState InventoryState { get; private set; }

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
        InventoryState = InventoryState.IDDLE;
        StartConfiguration();
    }

    /// <summary>
    /// Trigggered on first init
    /// </summary>
    public virtual void StartConfiguration() { }

    private void HandleInventoryChanged(InventoryItem activeInventory)
    {
        if (InventoryItemType == activeInventory?.InventoryItemType)
        {
            Enable();
        }
        else
        {
            Iddle();
        }
    }

    private void Iddle()
    {
        InventoryState = InventoryState.IDDLE;
    }

    private void Enable()
    {
        InventoryState = InventoryState.ACTIVE;
    }

    private void Disable()
    {
        InventoryState = InventoryState.DISABLE;
    }

    public void On_Click()
    {
        if (m_inventoryChannel.ActiveInventory?.InventoryItemType == InventoryItemType) return;
        m_inventoryChannel.RaiseEvent(this);
    }

    public void ChangeState(InventoryState state)
    {
        OnStateChanged(InventoryState, state);
        InventoryState = state;
    }

    PlaneBase GetPlane(RaycastHit2D[] rays)
    {
        if (rays.Length == 0) return null;
        var res = Array.Find(rays, val => val.collider.GetComponent<PlaneBase>() != null);
        if (!res) return null;
        return res.collider.GetComponent<PlaneBase>();
    }

    public virtual void OnStateChanged(InventoryState before, InventoryState after) { }
}

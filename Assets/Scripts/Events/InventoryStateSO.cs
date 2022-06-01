using UnityEngine.Events;
using UnityEngine;
using System;

/// <summary>
/// This class is used for Events that have a bool argument.
/// Example: An event to toggle a UI interface
/// </summary>

public enum InventoryCommand
{
    ACTIVE,
    DEACTIVE,
    LOCKED
}

[CreateAssetMenu(menuName = "Events/Inventory Event Channel")]
public class InventoryStateSO : DescriptionBaseSO
{
    public UnityAction<InventoryItem> OnEventRaised;

    public void RaiseEvent(InventoryItem value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
    }
}

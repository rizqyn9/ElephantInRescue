using UnityEngine.Events;
using UnityEngine;

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
    public UnityAction<InventoryCommand, InventoryItem> OnEventRaised;

    public void RaiseEvent(InventoryCommand cmd, InventoryItem value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(cmd, value);
    }
}

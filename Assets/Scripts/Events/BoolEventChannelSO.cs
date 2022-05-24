using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Events that have a bool argument.
/// Example: An event to toggle a UI interface
/// </summary>

[CreateAssetMenu(menuName = "Events/Bool Event Channel")]
public class BoolEventChannelSO : DescriptionBaseSO
{
    public UnityAction<bool> OnEventRaised;

    /// <summary>
    /// true value means increment root count
    /// false value means decrease root count
    /// </summary>
    /// <param name="value"></param>
    public void RaiseEvent(bool value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Bool Event Channel")]
public class StringEventChannelSO : DescriptionBaseSO
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}

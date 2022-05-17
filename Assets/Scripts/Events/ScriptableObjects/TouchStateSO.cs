using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TouchState
{
    INVENTORY,
    EMPTY,
    OTHER,
    GRID
}

[CreateAssetMenu(menuName = "Events/Touch State Event Channel")]
public class TouchStateSO : DescriptionBaseSO
{
    // Left params for lastState, right ref newState
    public UnityAction<TouchState> OnEventRaised;

    // Cache every new touched
    public List<TouchState> cachedState = new List<TouchState>() { TouchState.EMPTY };

    public TouchState GetLatestTouch => cachedState[cachedState.Count - 1];

    private void OnEnable()
    {
        ClearCache();
    }

    private void OnDisable()
    {
        ClearCache();
    }

    private void ClearCache()
    {
        cachedState = new List<TouchState>() { TouchState.EMPTY };
    }

    public void RaiseEvent(TouchState touched)
    {
        if (OnEventRaised != null)
        {
            cachedState.Add(touched);
            Debug.Log(cachedState);
            OnEventRaised.Invoke(touched);
        }
    }
}

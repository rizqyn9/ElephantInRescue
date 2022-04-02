using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    PLAY,
    PAUSE,
}

[CreateAssetMenu(menuName = "Events/Game State Event Channel")]
public class GameStateChannelSO : ScriptableObject
{
    public event UnityAction<GameState> OnEventRaised;

    public void RaiseEvent(GameState value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}

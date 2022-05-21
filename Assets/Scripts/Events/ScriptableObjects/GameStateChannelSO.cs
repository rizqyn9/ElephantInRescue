using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    BEFORE_PLAY,
    PLAY,
    PAUSE,
    FINISH
}

[CreateAssetMenu(menuName = "Events/Game State Event Channel")]
public class GameStateChannelSO : DescriptionBaseSO
{
    public event UnityAction<GameState> OnEventRaised;

    [SerializeField] GameState m_gameState;

    public GameState GameState { get => m_gameState; private set => m_gameState = value; }

    private void OnEnable()
    {
        Debug.Log("Instance");
        m_gameState = GameState.BEFORE_PLAY;
    }

    public void RaiseEvent(GameState gameState)
    {
        if (OnEventRaised != null && m_gameState != gameState)
            OnEventRaised.Invoke(gameState);
    }
}

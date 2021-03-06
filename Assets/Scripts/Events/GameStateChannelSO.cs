using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    BEFORE_PLAY,
    TUTORIAL,
    PLAY,
    PAUSE,
    FINISH,
}

[CreateAssetMenu(menuName = "Events/Game State Event Channel")]
public class GameStateChannelSO : DescriptionBaseSO
{
    public event UnityAction<GameState, GameState> OnEventRaised;

    [SerializeField] GameState m_gameState;

    public GameState GameState { get => m_gameState; private set => m_gameState = value; }

    private void OnEnable()
    {
        m_gameState = GameState.BEFORE_PLAY;
    }

    public void RaiseEvent(GameState gameState)
    {
        if (OnEventRaised != null && m_gameState != gameState)
        {
            OnEventRaised.Invoke(m_gameState, gameState);
            GameState = gameState;
        }
    }
}

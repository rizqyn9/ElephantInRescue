using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CivilianConfig
{
    public float Radius;
    [Range(0, 3)]public float Speed;
    public List<Plane> Routes;
}

[RequireComponent(typeof(BoxCollider2D))]
public class Civilian : MonoBehaviour
{
    [SerializeField] internal CivilianConfig CivilianConfig;
    [SerializeField] GameStateChannelSO m_gameStateChannel;
    [SerializeField] internal SpriteRenderer m_sprite;

    public Vector2 Direction { get; private set; }
    public CivilianMovement CivilianMovement { get; internal set; }

    internal virtual void SetDirection(Vector2 dir) {
        OnDirectionChange(dir);
        Direction = dir;
    }

    internal virtual void OnEnable() {
        CivilianMovement = new CivilianMovement(this);
        m_gameStateChannel.OnEventRaised += HandleGameStateOnChange;
    }

    internal virtual void OnDisable() {
        m_gameStateChannel.OnEventRaised -= HandleGameStateOnChange;
    }

    internal virtual void Start()
    {
    }

    private void HandleGameStateOnChange(GameState old, GameState recent)
    {
        OnGameStateChange(old, recent);
        switch (recent)
        {
            case GameState.BEFORE_PLAY:
                OnBeforePlay();
                break;
            case GameState.PLAY:
                OnPlay();
                break;
            case GameState.PAUSE:
                OnPause();
                break;
            case GameState.TUTORIAL:
                OnTutorial();
                break;
            case GameState.FINISH:
                OnFinish();
                break;
            default:
                throw new System.Exception("Not any handle");
        }
    }

    internal virtual void OnBeforePlay() { }

    internal virtual void OnTutorial() { }

    internal virtual void OnPause() { }

    internal virtual void OnPlay() { }

    internal virtual void OnFinish() { }

    internal virtual void OnGameStateChange(GameState old, GameState recent) { }

    internal virtual void OnDirectionChange(Vector2 dir) { }

    internal virtual void OnTriggerEnter2D(Collider2D collision) { }

    internal virtual void OnTriggerExit2D(Collider2D collision) { }

    internal virtual void OnHitObstacle() { }

    internal virtual void OnCanMoveChange() { }
}

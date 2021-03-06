using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CivilianConfig
{
    public float Radius;
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Civilian : MonoBehaviour
{
    [SerializeField] internal CivilianConfig CivilianConfig;
    [SerializeField] GameStateChannelSO m_gameStateChannel;
    [SerializeField] internal SpriteRenderer m_sprite;

    public Vector2 Direction { get; private set; }
    public bool CanSeePlayer { get; set; }

    public CivilianFOV CivilianFOV { get; internal set; }
    public Plane CurrentPlane { get; set; }

    internal virtual void SetDirection(Vector2 dir) {
        Direction = dir;
        OnDirectionChange(dir);
    }

    internal virtual void OnEnable() {
        Direction = Vector2.down;
        CanSeePlayer = false;
        CivilianFOV = new CivilianFOV(this);
        m_gameStateChannel.OnEventRaised += HandleGameStateOnChange;
    }

    internal virtual void OnDisable() {
        m_gameStateChannel.OnEventRaised -= HandleGameStateOnChange;
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

    internal virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Plane plane))
        {
            OnPlaneEnter(plane);
        } else if (collision.TryGetComponent(out PlayerController player))
        {
            SetCanSeePlayer(true);
        }
    }

    internal virtual void OnTriggerExit2D(Collider2D collision)
    {
        Plane plane = collision.GetComponent<Plane>();
        if (plane) OnPlaneExit(plane);
    }

    internal virtual void OnBeforePlay() { }

    internal virtual void OnTutorial() { }

    internal virtual void OnPause() { }

    internal virtual void OnPlay() { }

    internal virtual void OnFinish() { }

    internal virtual void OnGameStateChange(GameState old, GameState recent) { }

    internal virtual void OnDirectionChange(Vector2 dir) { }

    internal virtual void OnPlaneExit(Plane plane)
    {
        plane.SetCivilian(null);
    }

    internal virtual void OnPlaneEnter(Plane plane)
    {
        CheckValidPlane(plane);
        SetCurrentPlane(plane);
        plane.SetCivilian(this);
    }

    internal virtual void OnCanMoveChange() { }

    internal virtual void CheckValidPlane(Plane plane)
    {
        if (plane.Box) OnHitObstacle();
    }

    internal virtual void OnHitObstacle() { }

    public void SetCanSeePlayer (bool should)
    {
        CanSeePlayer = should;
        if (should)
            OnSeePlayer();
    }

    internal virtual void OnSeePlayer()
    {
        //PlayerController.Instance.OnHitCivilian(this);
    }

    internal void SetCurrentPlane(Plane plane)
    {
        CurrentPlane = plane;
    }
}

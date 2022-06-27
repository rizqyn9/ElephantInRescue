using UnityEngine;
using System.Collections.Generic;

public class CivilianBarier : Civilian, ICMovement
{
    private readonly List<string> RegisteredMotionState = new List<string>()
    {
        "ATTACK",
        "WALK"
    };

    public CivilianMovement CivilianMovement { get; set; }
    public bool CanMove { get; set; }

    public HumanoidAnimation<Civilian> AnimationController { get; set; }
    public Animator Animator { get => AnimationController.Animator; }

    internal override void OnEnable()
    {
        base.OnEnable();
        AnimationController = new HumanoidAnimation<Civilian>(this, RegisteredMotionState, GetComponentInChildren<Animator>());
        CivilianMovement = GetComponent<CivilianMovement>();

        m_sprite.enabled = false;
    }

    internal override void OnPlay()
    {
        base.OnPlay();
        m_sprite.enabled = true;

        CivilianFOV.Start();
        CivilianMovement.StartMove();
    }

    internal override void OnHitObstacle()
    {
        base.OnHitObstacle();
        StopAllCoroutines();
        CivilianMovement.ReverseDirection();
        CivilianMovement.StartMove();
    }

    public void OnReverse() { }

    internal override void OnDirectionChange(Vector2 dir)
    {
        base.OnDirectionChange(dir);
        AnimationController.UpdateStateMotion("WALK", Direction);
    }

    public void OnStartWalking() {}

    public void OnUpdateTarget() {}

    public void SetCanMove(bool shouldMove)
    {
        CanMove = shouldMove;
        if(!CanMove)
            Animator.speed = 0;
    }
}

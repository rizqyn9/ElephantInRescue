using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CivilianIdle))]
public class CivilianStun : Civilian
{
    [SerializeField] GameObject GOThrow;

    public CivilianIdle CivilianIdle { get; set; }
    public Throwable Throwable { get; set; }

    public HumanoidAnimation<CivilianStun> AnimationControl { get; private set; }
    private readonly List<string> RegisteredMotionState = new List<string>()
    {
        "ATTACK",
        "IDDLE"
    };

    internal override void OnEnable()
    {
        base.OnEnable();
        CivilianIdle = GetComponent<CivilianIdle>();
        AnimationControl = new HumanoidAnimation<CivilianStun>(this, RegisteredMotionState, GetComponentInChildren<Animator>());
        m_sprite.enabled = false;
    }

    internal override void OnBeforePlay()
    {
        base.OnBeforePlay();
    }

    internal override void OnPlay()
    {
        base.OnPlay();
        m_sprite.enabled = true;
        CivilianIdle.StartRotation();
        CivilianFOV.Start();
        AnimationControl.UpdateStateMotion("IDDLE", Direction);
    }

    internal override void OnDirectionChange(Vector2 dir)
    {
        base.OnDirectionChange(dir);
        AnimationControl.UpdateStateMotion("IDDLE", Direction);
    }

    internal override void OnFinish()
    {
        base.OnFinish();
        CivilianIdle.StopRotation();
    }

    internal override void OnSeePlayer()
    {
        if (Throwable) return;
        base.OnSeePlayer();

        Throwable = Instantiate(GOThrow, transform).GetComponent<Throwable>();

        Throwable.Init(this, PlayerController.Instance.transform);
        PlayerController.Instance.OnThrowed(Throwable);
    }
}

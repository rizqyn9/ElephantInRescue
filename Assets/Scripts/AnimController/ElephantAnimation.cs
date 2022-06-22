using System.Collections.Generic;
using UnityEngine;

public class ElephantAnimation : MonoBehaviour
{
    public PlayerController PlayerController { get; internal set; }
    public Vector2 Direction { get => PlayerController.Direction; }
    public Animator Animator { get; internal set; }

    // Enumurate for animation state
    public static string KNOCK { get => "KNOCK"; }
    public static string IDDLE { get => "IDDLE"; }
    public static string WALK { get => "WALK"; }

    private List<string> m_registeredMotionState = new List<string>() { KNOCK, IDDLE, WALK };

    private void OnEnable()
    {
        PlayerController = GetComponentInParent<PlayerController>();
        Animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        LeanTween
            .value(0, 1, 3f)
            .setOnStart(() =>
            {
                MotionStateUpdate(WALK);
            })
            .setOnComplete(() =>
            {
                MotionStateUpdate(IDDLE);
            });
    }

    public void Knock()
    {
        MotionStateUpdate(KNOCK);
    }

    public void Iddle()
    {
        MotionStateUpdate(IDDLE);
    }

    string m_latestStateMotion = null;
    public void MotionStateUpdate(string state)
    {
        if (m_latestStateMotion == KNOCK || !Animator) return;
        ConditionActive(state);
        Animator?.SetFloat("X", Direction.x);
        Animator?.SetFloat("Y", Direction.y);
    }

    void ConditionActive(string active)
    {
        if (m_latestStateMotion == active) return;
        if (!m_registeredMotionState.Contains(active))
            throw new System.Exception($"{active} not registered as animation state");

        m_latestStateMotion = active;

        m_registeredMotionState.ForEach(val =>
        {
            Animator?.SetBool(val, val == active);
        });
    }
}

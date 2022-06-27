using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimation<T> where T: class
{
    public T Component { get; private set; }
    public Animator Animator { get; private set; }
    public List<string> RegisteredMotionState { get; private set; }
    string m_latestStateMotion = null;

    public HumanoidAnimation(T objectGraphic, List<string> motionState, Animator animator)
    {
        Component = objectGraphic;
        Animator = animator;
        RegisteredMotionState = motionState;
    }

    public virtual void UpdateStateMotion(string state, Vector2 direction)
    {
        //if (m_latestStateMotion == ATTACK || !m_animator) return;
        ConditionActive(state);
        Animator?.SetFloat("X", direction.x);
        Animator?.SetFloat("Y", direction.y);
    }

    internal virtual void ConditionActive(string active)
    {
        if (m_latestStateMotion == active) return;
        if (!RegisteredMotionState.Contains(active))
            throw new System.Exception($"{active} not registered as animation state");

        m_latestStateMotion = active;

        RegisteredMotionState.ForEach(val =>
        {
            Animator?.SetBool(val, val == active);
        });
    }
}

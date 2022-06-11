using System.Collections.Generic;
using UnityEngine;

public class ElephantAnimation : MonoBehaviour
{
    [SerializeField] Vector3 m_direction;
    [SerializeField] Animator m_animator;

    // Enumurate for animation state
    public static string KNOCK { get => "KNOCK"; }
    public static string IDDLE { get => "IDDLE"; }
    public static string WALK { get => "WALK"; }

    private List<string> m_registeredMotionState = new List<string>() { KNOCK, IDDLE, WALK };

    private void OnEnable()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Walk(Vector3 direction)
    {
        m_direction = direction;
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

    public void Knock(Vector3 direction)
    {
        MotionStateUpdate(KNOCK);
    }

    public void Iddle(Vector3 direction)
    {
        m_direction = direction;
        MotionStateUpdate(IDDLE);
    }

    public void MotionStateUpdate(string state)
    {
        ConditionActive(state);
        m_animator.SetFloat("X", m_direction.x);
        m_animator.SetFloat("Y", m_direction.y);
    }

    void ConditionActive(string active)
    {
        if (!m_registeredMotionState.Contains(active))
            throw new System.Exception($"{active} not registered as animation state");

        m_registeredMotionState.ForEach(val =>
        {
            m_animator.SetBool(val, val == active);
        });
    }
}

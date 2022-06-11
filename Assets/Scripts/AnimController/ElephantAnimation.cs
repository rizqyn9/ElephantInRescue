using System.Collections.Generic;
using UnityEngine;


public class ElephantAnimation : MonoBehaviour
{
    [SerializeField] Vector3 m_direction;
    [SerializeField] Animator m_animator;

    public static string KNOCK { get => "KNOCK"; }
    public static string IDDLE { get => "IDDLE"; }
    public static string WALK { get => "WALK"; }

    private List<string> vs = new List<string>() { KNOCK, IDDLE, WALK };

    private void OnEnable()
    {
        m_animator = GetComponent<Animator>();
    }

    public void MotionStateUpdate()
    {
    }

    void conditionActive(string active)
    {
        vs.ForEach(val =>
        {
            m_animator.SetBool(val, val == active);
        });
    }
}

using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] bool isKnocked = false;
    [SerializeField] ElephantAnimation m_elephantAnimation;

    private void OnEnable()
    {
        m_elephantAnimation = GetComponent<ElephantAnimation>();
    }

    private void Update()
    {
        ListenWithKeys();   
    }

    void ListenWithKeys()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector3.right;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector3.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector3.left;

        if (Input.GetKeyDown(KeyCode.Space))
            m_elephantAnimation.Knock();


        if (direction == Vector3.zero) return;
        m_elephantAnimation.Walk();
    }
}

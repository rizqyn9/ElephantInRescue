using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Civilian))]
public class CivilianIdle : MonoBehaviour
{
    [SerializeField] internal Plane PlaneIdleArea;
    [SerializeField][Range(0,50)] float m_intervalRotation;
    [SerializeField] List<Vector2> m_registeredDirection = new List<Vector2>();

    public Vector2 Direction { get => Civilian.Direction; }
    public Civilian Civilian { get; private set; }
    public bool CanRotate { get; private set; }
    public int CurrentIndex { get; private set; }
    public Coroutine Coroutine { get; private set; }

    private void OnEnable()
    {
        Civilian = GetComponent<Civilian>();
        if (!PlaneIdleArea) throw new System.Exception("Plane position cant be empty");
        transform.position = PlaneIdleArea.transform.position;
        CurrentIndex = 0;
    }

    public void StartRotation()
    {
        if (Coroutine != null) return;
        CanRotate = true;
        UpdateDirection();
        Coroutine = StartCoroutine(IRotate());
    }


    public void NextTarget()
    {
        CurrentIndex++;
        if (CurrentIndex == m_registeredDirection.Count) CurrentIndex = 0;
    }

    public void StopRotation()
    {
        CanRotate = false;
        StopAllCoroutines();
    }

    IEnumerator IRotate()
    {
        while (CanRotate)
        {
            yield return new WaitForSeconds(m_intervalRotation);
            NextTarget();
            UpdateDirection();
        }
    }

    void UpdateDirection() => Civilian.SetDirection(m_registeredDirection[CurrentIndex]);
}

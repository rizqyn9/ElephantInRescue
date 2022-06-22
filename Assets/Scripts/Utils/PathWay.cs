using UnityEngine;
using System.Collections.Generic;

interface IPathWay
{
    void Move(PlaneBase start, PlaneBase stop);
    void OnStop();
    void OnChangeTarget(PlaneBase planeBase);
    Vector2 Direction { get; set; }
}

[System.Serializable]
public class PathWay
{
    PlaneBase m_startPlane;
    PlaneBase m_stopPlane;
    MonoBehaviour gameObject;
    IPathWay m_pathWay;
    int currentPath = 0;

    public List<PlaneBase> MoveArea { get; internal set; }

    public PathWay(PlaneBase start, PlaneBase stop, MonoBehaviour gameObject)
    {
        if (!(gameObject is IPathWay))
            throw new System.Exception($"{gameObject.name}not implemant of interface IPathWay");

        MoveArea = new List<PlaneBase>();
        this.m_startPlane = start;
        this.m_stopPlane = stop;
        this.gameObject = gameObject;
        m_pathWay = gameObject as IPathWay;

        if (start.name == stop.name) throw new System.Exception("sadasd");

        m_pathWay.Direction = GetDirection();
        GenerateMoveArea();
        StartMove();
    }

    Vector2 GetDirection() =>
        Utils.DecideDirection(m_startPlane.transform.position, m_stopPlane.transform.position);

    float GetDistance() =>
        Vector2.Distance(m_startPlane.transform.position, m_stopPlane.transform.position);

    void GenerateMoveArea()
    {
        RaycastHit2D[] hits =
            Physics2D
                .RaycastAll(gameObject.transform.position, m_pathWay.Direction, GetDistance());

        foreach (RaycastHit2D hit in hits)
        {
            PlaneBase plane = hit.collider.GetComponent<PlaneBase>();

            if (plane)
                MoveArea.Add(plane);
        }
    }

    public void StartMove()
    {
        if (currentPath < MoveArea.Count - 1)
        {
            currentPath++;
            m_pathWay.Move(MoveArea[currentPath - 1], MoveArea[currentPath]);
        }
        else
        {
            Debug.Log(m_pathWay.Direction);
            m_pathWay.OnStop();
        }
    }
}

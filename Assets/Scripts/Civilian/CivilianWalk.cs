using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseCivilian))]
public class CivilianWalk : MonoBehaviour
{
    [SerializeField] List<Plane> planeWayPoint;
    [SerializeField] [Range(0, 4)] float m_speed = 2f;
    [SerializeField] bool m_canMove = false;
    [SerializeField] int m_indexNow = 0;

    private void Start()
    {
        transform.position = planeWayPoint[0].transform.position;
        StartCoroutine(StartMove());
    }

    IEnumerator StartMove()
    {
        m_canMove = true;

        while (m_canMove)
        {
            while (m_indexNow <= planeWayPoint.Count - 1)
            {
                yield return StartCoroutine(Move(planeWayPoint[m_indexNow].transform));
                m_indexNow++;
            }
            if (m_indexNow <= planeWayPoint.Count) m_indexNow = 0;
        }
    }

    IEnumerator Move(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, m_speed * Time.deltaTime);
            yield return null;
        }
    }
}

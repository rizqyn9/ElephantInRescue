using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof (BaseCivilian))]
public class CivilianWalk : MonoBehaviour
{
    [SerializeField] Transform fromPlace, toPlace;
    [SerializeField] [Range(0, 4)] float m_speed = 2;

    private void Start()
    {
        transform.position = fromPlace.position;
    }

    private void Update()
    {
        Vector3 dir = toPlace.position - transform.position;
        transform.Translate(dir.normalized  * m_speed * Time.deltaTime, Space.Self);
    }
}

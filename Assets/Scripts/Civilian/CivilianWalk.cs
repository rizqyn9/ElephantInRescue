using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BaseCivilian))]
public class CivilianWalk : MonoBehaviour
{
    [SerializeField] List<Plane> planeWayPoint;
    [SerializeField] [Range(0, 4)] float m_speed = 2f;
    [SerializeField] bool m_canMove = false;
    [SerializeField] int m_indexNow = 0;
    [SerializeField] GameStateChannelSO m_gameStateChannel;
    SpriteRenderer m_renderer;

    private void OnEnable()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_renderer.enabled = false;
        m_gameStateChannel.OnEventRaised += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        m_gameStateChannel.OnEventRaised -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState before, GameState gameState)
    {
        switch (gameState)
        {
            case GameState.PLAY:
                if (before == GameState.PAUSE) break;
                m_renderer.enabled = true;
                StartCoroutine(StartMove());
                break;
            case GameState.FINISH:
                //Destroy(gameObject);
                break;
        }
    }

    private void Start()
    {
        transform.position = planeWayPoint[0].transform.position;
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
        OnChangeTarget(target);
        while (Vector3.Distance(transform.position, target.position) > 0.05f && m_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, m_speed * Time.deltaTime);
            yield return null;
        }
    }

    public void Stop()
    {
        m_canMove = false;
    }

    private void OnChangeTarget(Transform target)
    {
        Vector3 targ = target.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x -= objectPos.x;
        targ.y -= objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}

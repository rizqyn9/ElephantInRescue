using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BaseCivilian))]
public class CivilianWalk : MonoBehaviour
{
    [SerializeField] List<PlaneBase> planeWayPoint;
    [SerializeField] [Range(0, 4)] float m_speed = 2f;
    [SerializeField] bool m_canMove = false;
    [SerializeField] int m_indexNow = 0;
    [SerializeField] GameStateChannelSO m_gameStateChannel;
    BaseCivilian m_baseCivilian;
    SpriteRenderer m_renderer;

    private void OnEnable()
    {
        m_baseCivilian = GetComponent<BaseCivilian>();
        m_renderer = GetComponentInChildren<SpriteRenderer>();
        m_renderer.enabled = false;
        m_gameStateChannel.OnEventRaised += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        m_gameStateChannel.OnEventRaised -= HandleGameStateChanged;
    }

#if UNITY_EDITOR
    //private void OnValidate()
    //{
    //    gameObject.transform.position = planeWayPoint[0].transform.position;
    //}
#endif

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

        // Check valid moveable
        if (planeWayPoint.Count == 0) throw new System.Exception("plane way must greater more than 1");
    }

    int currentIndex = 0;
    int nextTarget = 1;
    int WayCount => planeWayPoint.Count; // 3

    void RecalculateTarget()
    {
        // NEXT
        if(currentIndex < nextTarget)
        {
            currentIndex++;
            if(nextTarget + 1 >= WayCount) // Reserve
            {
                nextTarget--;
            } else
            {
                nextTarget++;
            }
        }

        // PREV
        else if(currentIndex > nextTarget)
        {
            currentIndex--;
            if(nextTarget <= 0) //Reserve
            {
                nextTarget++;
            } else
            {
                nextTarget--;
            }
        }

        print($"{currentIndex} {nextTarget}");
    }

    IEnumerator StartMove()
    {
        m_canMove = true;

        while (m_canMove)
        {
            //Debug.Log("1");

            yield return
                StartCoroutine(
                    Move(planeWayPoint[nextTarget].transform,
                        () => {
                            RecalculateTarget();
                        }
                    ));
            m_indexNow++;
            //if (m_indexNow <= planeWayPoint.Count) m_indexNow = 0;
            //Debug.Log("2");
        }
    }

    IEnumerator Move(Transform target, System.Action cb)
    {
        Debug.Log("Start");
        m_baseCivilian.Direction = Utils.DecideDirection(transform.position, target.position);

        while (Vector3.Distance(transform.position, target.position) > 0.05f && m_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, m_speed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Finsi");
        cb();
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

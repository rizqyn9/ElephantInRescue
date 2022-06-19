using System.Collections;
using UnityEngine;

public class BaseCivilian : MonoBehaviour
{
    public CivilianWalk CivilianWalk { get; private set; }
    [SerializeField] float radius = 3f;
    [SerializeField] float angle = 360f;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] GameObject m_throwedGO;
    [SerializeField] Vector3 m_direction;

    CivilianAnimation m_civilianAnimation;

    public Vector3 Direction { get => m_direction; set => OnDirectionChanged(value); }

    private void OnEnable()
    {
        m_civilianAnimation = GetComponentInChildren<CivilianAnimation>();
    }

    private void OnDirectionChanged(Vector3 value)
    {
        m_direction = value;
        m_civilianAnimation.Walk(value);
    }

    public bool CanSeePlayer { get; private set; }

    private void Start()
    {
        CivilianWalk = GetComponent<CivilianWalk>();
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);

        while (true)
        {
            yield return wait;
            FOV2();
        }
    }

    private void FOV2()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Direction, radius);
        Debug.DrawRay(transform.position, Direction, Color.red);

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                OnObstacleHit(hit);
                break;
            }

            if (hit.collider.CompareTag("Player"))
            {
                OnPlayerHit();
                break;
            }
        }
    }

    private void OnPlayerHit()
    {
        m_civilianAnimation.Attack(m_direction);
        ThrowSomething();
        PlayerController.Instance.OnHitCivilian(this);
        CivilianWalk.Stop();
    }

    private void OnObstacleHit(RaycastHit2D obstacle)
    {
        Box box = obstacle.collider.GetComponent<Box>();
        if (box)
        {
            CivilianWalk.OnHitBox();
        }
    }

    void ThrowSomething()
    {
        if (!m_throwedGO) return;
        GameObject throwed = Instantiate(m_throwedGO, transform);
        LeanTween.move(throwed, PlayerController.Instance.gameObject.transform.position, 2f);
    }

    #region Not used now
    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        //Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        //Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        //Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);

        //if (CanSeePlayer)
        //{
        //    Gizmos.color = Color.green;
        //    if (PlayerController.Instance == null) return;
        //    Gizmos.DrawLine(transform.position, PlayerController.Instance.transform.position);
        //}

        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);

    }
#endif
    #endregion

    private void FOV()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, radius, targetLayer);

        if (target && target.CompareTag("Player"))
        {
            Vector2 directionToTarget = (transform.position - PlayerController.Instance.transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) > angle / 2)
            {
                CanSeePlayer = true;
                OnPlayerHit();
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else
        {
            CanSeePlayer = false;
        }
    }

}

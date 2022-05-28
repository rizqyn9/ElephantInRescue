using System.Collections;
using UnityEngine;

public class BaseCivilian : MonoBehaviour
{
    public CivilianWalk CivilianWalk { get; private set; }
    [SerializeField] float radius = 3f;
    [SerializeField] float angle = 360f;
    [SerializeField] LayerMask targetLayer;

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
            FOV();
        }
    }

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

    private void OnPlayerHit()
    {
        PlayerController.Instance.OnHitCivilian(this);
        CivilianWalk.Stop();
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.green;
            if (PlayerController.Instance == null) return;
            Gizmos.DrawLine(transform.position, PlayerController.Instance.transform.position);
        }
    }
#endif
}

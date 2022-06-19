using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Box : MonoBehaviour
{
    [SerializeField] GameStateChannelSO m_GameStateChannelSO = null;
    [SerializeField] PlaneBase m_boxPlane; // Initialize position
    [SerializeField] LayerMask m_LayerGrid;

    List<Vector2> m_dirs = new List<Vector2>() { Vector2.down, Vector2.up, Vector2.left, Vector2.right };
    List<Plane> m_activePlanes = new List<Plane>();

    public bool IsActive { get; private set; }

    private void Start()
    {
        transform.position = m_boxPlane.transform.position;
        IsActive = false;
        m_boxPlane.SetBox(this);
    }

    private void Update()
    {
        if (!IsActive) return;

        if (IsActive)
            SetFocus();
        // Close click away
        if (IsActive && Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Utils.RaycastCamera(Input.mousePosition, Vector2.zero, gameObject.layer);
            if (hit.collider.GetComponent<Box>() == this) return;
            CloseFocus();
        }
    }

    private void OnMouseDown()
    {
        if (IsActive && m_activePlanes.Count != 0)
            CloseFocus();

        else if (!IsActive) SetFocus();
    }

    public void MoveToPlane(PlaneBase plane)
    {
        CloseFocus();
        LeanTween
            .move(gameObject, plane.transform.position, .5f)
            .setOnStart(CloseFocus)
            .setOnComplete(() =>
            {
                m_boxPlane = plane;
                m_boxPlane.SetBox(this);
            });
    }

    void SetFocus()
    {
        m_activePlanes = new List<Plane>();
        foreach (Vector2 dir in m_dirs)
            NotifyActivePlaneBaseByDir(dir);

        IsActive = true;
    }

    void NotifyActivePlaneBaseByDir(Vector2 dir)
    {
        RaycastHit2D[] targets = Physics2D.RaycastAll(transform.position, dir, 1f, m_LayerGrid);

        Plane plane = null;
        RaycastHit2D target =
            System.Array.Find(targets, ((planeTarget) => {
                plane = planeTarget.collider.GetComponent<Plane>();
                return (bool)(plane && plane.name != m_boxPlane.name);
            }));

        if (!target || !plane || plane.Civilian || plane.PlaneType != PlaneTypeEnum.ROUTE) return;

        plane.SetFocus(true, this);
        m_activePlanes.Add(plane);
    }


    void CloseFocus ()
    {
        m_activePlanes.ForEach(plane => plane.SetFocus(false, this));
        m_activePlanes = new List<Plane>(); // Reset

        IsActive = false;
    }

#if UNITY_EDITOR
    [ContextMenu("Validate Position")]
    void ValidatePosition() => transform.position = m_boxPlane.transform.position;
#endif
}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Box : MonoBehaviour
{
    [SerializeField] GameStateChannelSO m_GameStateChannelSO = null;
    [SerializeField] PlaneBase m_boxPlane; // Initialize position
    [SerializeField] LayerMask m_LayerGrid;

    List<Vector2> m_dirs = new List<Vector2>() { Vector2.down, Vector2.up, Vector2.left, Vector2.right };
    List<PlaneBase> m_PlaneTargets = new List<PlaneBase>();

    public bool IsActive { get; set; }

    private void Start()
    {
        transform.position = m_boxPlane.transform.position;
        m_boxPlane?.OnBox(this);
    }

    private void Update()
    {
        if (!IsActive) return;

        // If active & click away notify box false
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Utils.RaycastCamera(Input.mousePosition, Vector2.zero, gameObject.layer);
            if (hit.collider.GetComponent<Box>() == this) return;
            CloseFocus(); 
        }
    }

    private void OnMouseDown()
    {
        IsActive = !IsActive;   // Toggle

        if(IsActive)
            m_PlaneTargets = new List<PlaneBase>();
            foreach (Vector2 dir in m_dirs)
                NotifyActivePlaneBaseByDir(dir);

        if (!IsActive && m_PlaneTargets.Count != 0)
            CloseFocus();
    }

    void NotifyActivePlaneBaseByDir(Vector2 dir)
    {
        RaycastHit2D[] targets = Physics2D.RaycastAll(transform.position, dir, 1f, m_LayerGrid);

        PlaneBase planeBase = null;
        RaycastHit2D target =
            System.Array.Find(targets, (plane) => {
                planeBase = plane.collider.GetComponent<PlaneBase>();
                return planeBase && planeBase.name != m_boxPlane.name;
            });

        if (!target || !planeBase) return;

        planeBase.OnBoxNotify(true, this);
        m_PlaneTargets.Add(planeBase);
    }

    public void MoveToPlane(PlaneBase plane)
    {
        LeanTween
            .move(gameObject, plane.transform.position, .5f)
            .setOnStart(CloseFocus)
            .setOnComplete(() =>
            {
                m_boxPlane = plane;
                m_boxPlane.OnBox(this);
            });
    }

    void CloseFocus ()
    {
        IsActive = false;
        m_PlaneTargets.ForEach(plane => plane.OnBoxNotify(false, this));
        m_PlaneTargets = new List<PlaneBase>();
    }

#if UNITY_EDITOR
    [ContextMenu("Validate Position")]
    void ValidatePosition() => transform.position = m_boxPlane.transform.position;
#endif
}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Box : MonoBehaviour
{
    [SerializeField] GameStateChannelSO m_GameStateChannelSO = null;
    [SerializeField] Plane m_boxPlane; // Initialize position
    [SerializeField] LayerMask m_LayerGrid;

    List<Plane> m_activePlanes = new List<Plane>();
    bool m_onMove = false;

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

        // Close click away
        if (IsActive && Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Utils.RaycastCamera(Input.mousePosition, Vector2.zero, gameObject.layer);
            if (hit.collider?.GetComponent<Box>() == this) return;
            CloseFocus();
        }
    }


    private void OnMouseDown()
    {
        if (m_onMove) return;
        if (IsActive)
        {
            IsActive = false;
            CloseFocus();
        }

        else if (!IsActive)
        {
            IsActive = true;
            SetFocus();
        }
    }

    public void MoveToPlane(Plane plane)
    {
        CloseFocus();
        IsActive = false;

        m_onMove = true;
        m_boxPlane.SetBox(null);        // UnSub

        m_boxPlane = plane;             // Sub for new Plane
        m_boxPlane.SetBox(this);
        LeanTween
            .move(gameObject, plane.transform.position, .5f)
            .setOnStart(CloseFocus)
            .setOnComplete(() =>
            {
                m_onMove = false;
            });
    }

    public void Refocus()
    {
        CloseFocus();
        if(IsActive)
            SetFocus();
    }


    void SetFocus()
    {
        m_activePlanes = PlaneUtils.GetAllAxis(m_boxPlane);
        m_activePlanes.ForEach(plane => plane.PlaneState.SetFocus(true, this));
    }


    void CloseFocus ()
    {
        m_activePlanes.ForEach(plane => plane.PlaneState.SetFocus(false, this));
        m_activePlanes = new List<Plane>(); // Reset
    }

#if UNITY_EDITOR
    [ContextMenu("Validate Position")]
    void ValidatePosition() => transform.position = m_boxPlane.transform.position;
#endif
}

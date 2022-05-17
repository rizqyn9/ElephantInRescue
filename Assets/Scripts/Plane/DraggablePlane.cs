using UnityEngine;

[RequireComponent(typeof(Plane))]
public class DraggablePlane : MonoBehaviour
{
    public bool PlaneActive { get; set; }

    [SerializeField] Plane m_Plane;
    [SerializeField] TouchStateSO m_TouchStateChannel;

    private void OnValidate()
    {
        m_Plane = GetComponent<Plane>();
    }

    private void OnMouseDown()
    {
        m_TouchStateChannel.RaiseEvent(TouchState.GRID);
        PlaneActive = !PlaneActive;
    }
}

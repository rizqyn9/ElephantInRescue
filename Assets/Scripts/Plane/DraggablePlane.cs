using UnityEngine;

public class DraggablePlane : MonoBehaviour
{
    [SerializeField] Plane m_Plane;
    [SerializeField] bool m_ActivatePlane;
    [SerializeField] TouchStateSO m_TouchStateChannel;

    private void OnValidate()
    {
        m_Plane = gameObject.GetComponent<Plane>();
    }

    private void OnMouseDown()
    {
        m_TouchStateChannel.RaiseEvent(TouchState.GRID);
        m_ActivatePlane = !m_ActivatePlane;
    }
}

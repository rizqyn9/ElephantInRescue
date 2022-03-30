using UnityEngine;

public enum PlaneType {
    NULL,
    WALL,
    ROUTE,
    TREE
}

public class Plane : MonoBehaviour
{
    public PlaneType planeType = PlaneType.NULL;

    public void SetPlaneType(PlaneType _planeType)
    {
        planeType = _planeType;
    }
}

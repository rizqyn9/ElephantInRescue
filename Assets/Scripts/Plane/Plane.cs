using UnityEngine;

public enum PlaneType {
    NULL,
    WALL,
    ROUTE,
    TREE
}

public class Plane : MonoBehaviour
{
    [SerializeField] PlaneType planeType = PlaneType.NULL;

    // Accessor
    public PlaneType PlaneType { get => planeType; }

    private void Start()
    {
        RegisterPlaneToPlaneManager();
    }

    public void RegisterPlaneToPlaneManager()
    {
        PlaneManager.Instance.AuthorizedPlane(this, out string planeNameHashed);
        gameObject.name = planeNameHashed;
    }
}

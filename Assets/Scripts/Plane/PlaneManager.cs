using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    private static PlaneManager _instance;
    public static PlaneManager Instance { get => _instance; }

    [SerializeField] Plane m_PlayerPlaneInstance = null;
    public Plane PlayerPlaneInstance { get => m_PlayerPlaneInstance; private set => m_PlayerPlaneInstance = value; }

    public readonly Dictionary<string, Plane> Planes = new Dictionary<string, Plane>();

    public InventoryItem ActiveInventory { get; private set; }
    private void HandleInventoryChanged(InventoryItem active)
    {
        ActiveInventory = active;
    }

    public void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;
    }

    static int planeIndex = 0;
    public static void AuthorizedPlane(Plane plane, out string planeHashed)
    {
        planeHashed = $"Plane-{planeIndex++}";

        if (plane.IsPlayerInstancePlace)
        {
            Instance.PlayerPlaneInstance = plane;
        }

        plane.name = planeHashed;
        Instance.Planes.Add(planeHashed, plane);
    }
}

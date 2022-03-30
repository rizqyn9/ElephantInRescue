using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    private static PlaneManager _instance;
    public static PlaneManager Instance { get => _instance; }

    [SerializeField] GameObject nullPlane, treePlane, wallPlane, routePlane;

    private Dictionary<string, Plane> planes = new Dictionary<string, Plane>();
    public Dictionary<string, Plane> Planes { get => planes; }

    public void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    [SerializeField] int planeIndex = 0;
    public void AuthorizedPlane(Plane _plane, out string planeHashed)
    {
        planeHashed = $"Plane-{planeIndex++}";
        planes.Add(planeHashed, _plane);
    }

}

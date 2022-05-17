using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    private static PlaneManager _instance;
    public static PlaneManager Instance { get => _instance; }

    [SerializeField] GameObject playerGO;

    [SerializeField] static Plane m_playerStartInstance = null;

    [SerializeField] public GameObject InstancedPlayer = null;

    public readonly Dictionary<string, Plane> Planes = new Dictionary<string, Plane>();

    public void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;
    }

    static int planeIndex = 0;
    public static void AuthorizedPlane(Plane _plane, out string planeHashed)
    {
        planeHashed = $"Plane-{planeIndex++}";

        if (m_playerStartInstance == null && _plane.IsPlayerInstancePlace)
        {
            m_playerStartInstance = _plane;
            Instance.InstancedPlayer = Instantiate(Instance.playerGO, _plane.transform.position, Quaternion.identity);
        }

        Instance.Planes.Add(planeHashed, _plane);
    }
}

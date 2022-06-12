using UnityEngine;

public enum PlaneTypeEnum
{
    ROUTE,
    TREE,
    FINISH,
    HOLE
}

[System.Serializable]
[RequireComponent(typeof(BoxCollider2D))]
public class Plane : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] internal InventoryStateSO m_inventoryChannel;

    [SerializeField] bool m_isPlayerInstancePlace = false;

    public PlaneTypeEnum PlaneType { get; internal set; }
    public bool IsPlayerInstancePlace { get => m_isPlayerInstancePlace; }
    public InventoryItem ActiveInventory { get; private set; }

    internal virtual void OnEnable()
    {
        m_inventoryChannel.OnEventRaised += HandleInventoryChange;
    }

    internal virtual void OnDisable()
    {
        m_inventoryChannel.OnEventRaised -= HandleInventoryChange;
    }

    internal virtual void Start()
    {
        CheckValidity();
        RegisterPlaneToPlaneManager();
    }

    void CheckValidity()
    {
        if (m_isPlayerInstancePlace && PlaneType != PlaneTypeEnum.ROUTE)
            throw new System.Exception("Instanced place but Plane type not use Route Plane");
    }

    void HandleInventoryChange(InventoryItem activeInventory)
    {
        ActiveInventory = activeInventory;
    }

    public virtual void OnElephant() { }

    public void RegisterPlaneToPlaneManager()
    {
        PlaneManager.AuthorizedPlane(this, out string planeNameHashed);
        gameObject.name = planeNameHashed;
    }

    internal virtual void OnMouseDown() { }
}

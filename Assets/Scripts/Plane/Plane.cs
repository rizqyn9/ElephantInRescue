using UnityEngine;

public enum PlaneTypeEnum
{
    ROUTE,
    TREE,
    FINISH
}

[RequireComponent(typeof(BoxCollider2D))]
public class Plane : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] internal InventoryStateSO m_inventoryChannel;

    [SerializeField] bool m_isPlayerInstancePlace = false;

    public PlaneTypeEnum PlaneType { get; internal set; }
    public bool IsPlayerInstancePlace { get => m_isPlayerInstancePlace; }

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

    public InventoryItem ActiveInventory { get; private set; }

    void HandleInventoryChange(InventoryItem activeInventory)
    {
        if(PlaneType == PlaneTypeEnum.TREE)
        {
            print(activeInventory.gameObject.name);
        }
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

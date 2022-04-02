using UnityEngine;

public enum PlaneType
{
    NULL,
    WALL,
    ROUTE,
    TREE
}

public class Plane : MonoBehaviour
{
    [SerializeField] PlaneType planeType = PlaneType.NULL;

    [Header("Events")]
    [SerializeField] InventoryStateSO _inventoryChannel = default;

    // Accessor
    public PlaneType PlaneType { get => planeType; }

    private void OnEnable()
    {
        _inventoryChannel.OnEventRaised += HandleInventoryChange;
    }

    private void OnDisable()
    {
        _inventoryChannel.OnEventRaised -= HandleInventoryChange;        
    }

    private void Start()
    {
        RegisterPlaneToPlaneManager();
    }

    void HandleInventoryChange(InventoryCommand cmd, InventoryItem item)
    {
        switch (cmd)
        {
            case InventoryCommand.ACTIVE:
                if(planeType == PlaneType.WALL && item.InventoryItemType == InventoryItemType.TEST2)
                {
                    Destroy(gameObject);
                } 
                break;


        }
    }

    public void RegisterPlaneToPlaneManager()
    {
        PlaneManager.Instance.AuthorizedPlane(this, out string planeNameHashed);
        gameObject.name = planeNameHashed;
    }
}

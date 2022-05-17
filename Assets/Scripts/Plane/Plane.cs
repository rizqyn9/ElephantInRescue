using UnityEditor.PackageManager;
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
    [SerializeField] InventoryStateSO m_inventoryChannel = default;

    [SerializeField] bool m_isPlayerInstancePlace = false;

    public PlaneTypeEnum PlaneType { get; internal set; }
    public bool IsPlayerInstancePlace { get => m_isPlayerInstancePlace; }

    private void OnEnable()
    {
        m_inventoryChannel.OnEventRaised += HandleInventoryChange;
    }

    private void OnDisable()
    {
        m_inventoryChannel.OnEventRaised -= HandleInventoryChange;
    }

    private void OnValidate()
    {
    }

    private void Start()
    {
        CheckValidity();
        RegisterPlaneToPlaneManager();
    }

    void CheckValidity()
    {
        if(m_isPlayerInstancePlace && PlaneType != PlaneTypeEnum.ROUTE)
            throw new System.Exception("Instanced place but Plane type not use Route Plane");

    }

    void HandleInventoryChange(InventoryCommand cmd, InventoryItem item)
    {
        switch (cmd)
        {
            case InventoryCommand.ACTIVE:
                if (PlaneType == PlaneTypeEnum.TREE && item.InventoryItemType == InventoryItemType.TEST2)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    public virtual void OnElephant() { }

    public void RegisterPlaneToPlaneManager()
    {
        PlaneManager.AuthorizedPlane(this, out string planeNameHashed);
        gameObject.name = planeNameHashed;
    }
}

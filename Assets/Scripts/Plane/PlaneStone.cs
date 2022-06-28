using UnityEngine;

public class PlaneStone : Plane
{
    [SerializeField] SpriteRenderer m_stoneRenderer;
    [SerializeField] InventorySO m_stoneSO;

    public bool Destroyed { get; private set; }

    internal override void Start()
    {
        SetPlaneType(PlaneTypeEnum.STONE);
        base.Start();
    }

    internal override void OnMouseDown()
    {
        base.OnMouseDown();

        if(!Destroyed && ActiveInventory?.InventoryItemType == InventoryItemType.HAMMER)
        {
            DestroyStone();
        }
    }

    private void DestroyStone()
    {
        Destroyed = true;
        SetPlaneType(PlaneTypeEnum.ROUTE);    
        Destroy(m_stoneRenderer.gameObject);
        m_stoneSO.Add();
        m_inventoryChannel.RaiseEvent(null);
    }
}

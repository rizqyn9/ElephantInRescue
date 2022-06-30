using UnityEngine;

public class PlaneStone : Plane
{
    [SerializeField] SpriteRenderer m_stoneRenderer;
    [SerializeField] InventorySO m_stoneSO;
    [SerializeField] AudioClip m_sfxStoneDestroy;

    public bool Destroyed { get; private set; }

    internal override void OnEnable()
    {
        SetPlaneType(PlaneTypeEnum.STONE);
        base.OnEnable();
        Destroyed = false;
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
        SoundManager.PlaySound(m_sfxStoneDestroy);
        Destroyed = true;
        SetPlaneType(PlaneTypeEnum.ROUTE);    
        Destroy(m_stoneRenderer.gameObject);
        m_stoneSO.Add();
        m_inventoryChannel.RaiseEvent(null);
    }
}

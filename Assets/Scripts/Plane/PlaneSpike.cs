using UnityEngine;

public class PlaneSpike : Plane
{
    [SerializeField] InventorySO m_stoneController;
    [SerializeField] GameObject m_effectBuild;
    [SerializeField] Sprite m_MoveableSprite;

    public BoxCollider2D BoxCollider2D { get; private set; }

    internal override void OnEnable()
    {
        SetPlaneType(PlaneTypeEnum.SPIKE);
        base.OnEnable();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    internal override void OnMouseDown()
    {
        base.OnMouseDown();
        if (ActiveInventory?.InventoryItemType == InventoryItemType.STONE)
        {
            Building();
            m_stoneController.Remove();
        }
    }

    void Building()
    {
        GameObject goEffect = Instantiate(m_effectBuild, BoxCollider2D.bounds.center, Quaternion.identity);
        Utils.BuildingEffect(null, () => {
            SetPlaneType(PlaneTypeEnum.ROUTE);
            m_groundRenderer.sprite = m_MoveableSprite;
            Destroy(goEffect);
        });
    }
}

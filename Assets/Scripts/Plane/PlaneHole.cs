using UnityEngine;

public class PlaneHole : Plane
{
    [SerializeField] InventorySO m_stoneController;
    [SerializeField] GameObject m_effectBuild;

    public BoxCollider2D BoxCollider2D { get; set; }

    internal override void OnEnable()
    {
        SetPlaneType(PlaneTypeEnum.HOLE);
        base.OnEnable();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    internal override void OnMouseDown()
    {
        base.OnMouseDown();
        if(ActiveInventory?.InventoryItemType == InventoryItemType.STONE)
        {
            Building();
            m_stoneController.Remove();
        }
    }

    void Building()
    {
        GameObject goEffect = Instantiate(m_effectBuild, BoxCollider2D.bounds.center, Quaternion.identity);
        LeanTween
            .value(0, 1, 3f)
            .setOnComplete(() => {
                SetPlaneType(PlaneTypeEnum.ROUTE);
                Destroy(goEffect);
            });
    }
}

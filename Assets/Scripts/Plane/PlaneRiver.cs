using UnityEngine;

public class PlaneRiver : Plane
{
    [SerializeField] GameObject m_effectBuild, m_bridge;
    [SerializeField] InventorySO m_rootController;

    public bool CanPassable { get; internal set; }
    public BoxCollider2D BoxCollider2D { get; private set; }

    internal override void OnEnable()
    {
        SetPlaneType(PlaneTypeEnum.HOLE); // Prevent passable
        base.OnEnable();
        CanPassable = false;            // Wait until bridge has success builded
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Create bridge on Mouse Down
    internal override void OnMouseDown()
    {
        base.OnMouseDown();

        if(ActiveInventory?.InventoryItemType == InventoryItemType.ROOT)
        {
            BuildBridge();
            m_rootController.Remove();
        }
    }

    void BuildBridge()
    {
        if (CanPassable) return;

        GameObject goEffect= Instantiate(m_effectBuild, BoxCollider2D.bounds.center, Quaternion.identity);
        LeanTween
            .value(0, 1, 3f)
            .setOnComplete(() => { CbAfterBuild(goEffect); });
    }

    void CbAfterBuild (GameObject effect)
    {
        GameObject bridge = Instantiate(m_bridge, BoxCollider2D.bounds.center, Quaternion.identity);
        
        LeanTween
            .alpha(effect, 0, .5f)                    
            .setOnComplete(() => {
                SetPlaneType(PlaneTypeEnum.ROUTE);
                CanPassable = true;
                Destroy(effect);
            });                
    }
}

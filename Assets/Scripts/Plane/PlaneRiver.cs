using UnityEngine;

public class PlaneRiver : Plane
{
    [SerializeField] GameObject m_effectBuild, m_bridge;
    [SerializeField] InventorySO m_rootController;
    BoxCollider2D m_boxCollider2D;

    public bool CanPassable { get; internal set; }

    internal override void OnEnable()
    {
        base.OnEnable();
        CanPassable = false;    // Wait until bridge has success builded
        m_boxCollider2D = GetComponent<BoxCollider2D>();
    }

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

        GameObject goEffect= Instantiate(m_effectBuild, m_boxCollider2D.bounds.center, Quaternion.identity);
        LeanTween
            .value(0, 1, 3f)
            .setOnComplete(() => { CbAfterBuild(goEffect); });
    }

    void CbAfterBuild (GameObject effect)
    {
        GameObject bridge = Instantiate(m_bridge, m_boxCollider2D.bounds.center, Quaternion.identity);
        
        LeanTween
            .alpha(effect, 0, .5f)                    
            .setOnComplete(() => {
                Destroy(effect);
            });                
    }
}

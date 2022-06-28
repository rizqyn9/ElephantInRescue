using UnityEngine;

public class PlaneTree : Plane
{
    [Header("Properties")]
    [SerializeField] bool m_ShouldDestroyable = false;
    [SerializeField] Sprite m_destroyableTree, m_notDestroyableTree;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] InventorySO m_rootSO;
    [SerializeField] SpriteRenderer m_groundSprite;
    [SerializeField] Color m_activeColor;

    public bool Destroyed { get; private set; }
    public bool ShouldDestroyable { get => m_ShouldDestroyable; private set => m_ShouldDestroyable = value; }

    internal override void Start()
    {
        SetPlaneType(PlaneTypeEnum.TREE);
        base.Start();
        Destroyed = false;
        if (ShouldDestroyable) m_spriteRenderer.sprite = m_destroyableTree;
        else m_spriteRenderer.sprite = m_notDestroyableTree;
    }

    internal override void OnMouseDown()
    {
        base.OnMouseDown();

        if (m_ShouldDestroyable && ActiveInventory?.InventoryItemType == InventoryItemType.KNIFE)
        {
            Destroyed = true;
            SetPlaneType(PlaneTypeEnum.ROUTE);
            m_spriteRenderer.enabled = false;
            m_rootSO.Add();
            m_inventoryChannel.RaiseEvent(null);
        }
    }
}

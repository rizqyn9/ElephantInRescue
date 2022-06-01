using UnityEngine;

public class PlaneTree : Plane
{
    [Header("Properties")]
    [SerializeField] bool m_ShouldDestroyable = false;
    [SerializeField] Sprite m_destroyableTree, m_notDestroyableTree;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] BoolEventChannelSO m_channelRootCount;

    public bool ShouldDestroyable { get => m_ShouldDestroyable; private set => m_ShouldDestroyable = value; }

    public override void Start()
    {
        base.Start();
        PlaneType = PlaneTypeEnum.TREE;
        if (ShouldDestroyable) m_spriteRenderer.sprite = m_destroyableTree;
        else m_spriteRenderer.sprite = m_notDestroyableTree;
    }

    public override void OnMouseDown()
    {
        //if (m_ShouldDestroyable && m_inventoryChannel.ActiveInventory?.InventoryItemType == InventoryItemType.KNIFE)
        //{
        //    m_spriteRenderer.enabled = false;
        //    m_channelRootCount.RaiseEvent(true);
        //}
        base.OnMouseDown();
    }
}

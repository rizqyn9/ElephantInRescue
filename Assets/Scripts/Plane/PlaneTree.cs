using UnityEngine;

public class PlaneTree : Plane
{
    [Header("Properties")]
    [SerializeField] bool m_ShouldDestroyable = false;
    [SerializeField] Sprite m_destroyableTree, m_notDestroyableTree;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] BoolEventChannelSO m_channelRootCount;

    public bool Destroyed { get; private set; }

    public bool ShouldDestroyable { get => m_ShouldDestroyable; private set => m_ShouldDestroyable = value; }

    internal override void OnEnable()
    {
        base.OnEnable();        
    }

    internal override void OnDisable()
    {
        base.OnDisable();
    }

    internal override void Start()
    {
        base.Start();
        Destroyed = false;
        if (ShouldDestroyable) m_spriteRenderer.sprite = m_destroyableTree;
        else m_spriteRenderer.sprite = m_notDestroyableTree;
    }

    [SerializeField] InventoryItem item;
    internal override void OnMouseDown()
    {
        base.OnMouseDown();

        if (m_ShouldDestroyable && ActiveInventory?.InventoryItemType == InventoryItemType.KNIFE)
        {
            m_spriteRenderer.enabled = false;
            m_channelRootCount.RaiseEvent(true);
            m_inventoryChannel.RaiseEvent(null);
        }
    }
}

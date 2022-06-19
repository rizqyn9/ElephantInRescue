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
        PlaneType = PlaneTypeEnum.TREE;
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
            PlaneType = PlaneTypeEnum.ROUTE;
            m_spriteRenderer.enabled = false;
            m_rootSO.Add();
            m_inventoryChannel.RaiseEvent(null);
        }
        base.OnMouseDown();
        if (Box)
        {
            Box.MoveToPlane(this);
        }
    }

    internal override void OnFocusChanged()
    {
        base.OnFocusChanged();
        print(IsFocus);
        SetBox(IsFocus ? Box : null);
        m_groundSprite.color = IsFocus ? m_activeColor : Color.white;
    }

    public override void SetCivilian(BaseCivilian civilian)
    {
        base.SetCivilian(civilian);
        SetFocus(false, Box);
    }
}

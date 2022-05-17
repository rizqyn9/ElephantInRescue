using UnityEngine;

public class PlaneTree : Plane
{
    [Header("Properties")]
    [SerializeField] bool m_ShouldDestroyable = false;
    [SerializeField] Sprite destroyableTree, notDestroyableTree;
    [SerializeField] SpriteRenderer spriteRenderer;

    public bool ShouldDestroyable { get => m_ShouldDestroyable; private set => m_ShouldDestroyable = value; }

    public override void Start()
    {
        base.Start();
        PlaneType = PlaneTypeEnum.TREE;
        if (ShouldDestroyable) spriteRenderer.sprite = destroyableTree;
        else spriteRenderer.sprite = notDestroyableTree;
    }
}

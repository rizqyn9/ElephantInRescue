using UnityEngine;

public class PlaneBase : Plane
{
    [SerializeField] Color m_activeColor;
    SpriteRenderer m_sprite;

    internal override void Start()
    {
        SetPlaneType(PlaneTypeEnum.ROUTE);
        m_sprite = GetComponentInChildren<SpriteRenderer>();
        base.Start();
    }

    public override void SetCivilian(BaseCivilian civilian)
    {
        base.SetCivilian(civilian);
    }
}

using UnityEngine;

public class PlaneBase : Plane
{
    [SerializeField] Color m_activeColor;
    SpriteRenderer m_sprite;

    internal override void Start()
    {
        base.Start();
        PlaneType = PlaneTypeEnum.ROUTE;
        m_sprite = GetComponentInChildren<SpriteRenderer>();
    }

    internal override void OnMouseDown()
    {
        base.OnMouseDown();
        if(Box)
        {
            Box.MoveToPlane(this);
        }
    }

    internal override void OnFocusChanged()
    {
        base.OnFocusChanged();
        print(IsFocus);
        SetBox(IsFocus ? Box : null);
        m_sprite.color = IsFocus ? m_activeColor : Color.white;
    }

    public override void SetCivilian(BaseCivilian civilian)
    {
        base.SetCivilian(civilian);
        SetFocus(false, Box);
    }


}

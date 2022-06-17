using UnityEngine;

public class PlaneBase : Plane
{
    [SerializeField] Color m_activeColor;
    bool m_isActive = false;
    SpriteRenderer m_sprite; 

    public Box Box { get; set; }

    internal override void Start()
    {
        base.Start();
        m_sprite = GetComponentInChildren<SpriteRenderer>();
        Box = null;
    }

    internal override void OnMouseDown()
    {
        base.OnMouseDown();
        if(m_isActive && Box)
        {
            Box.MoveToPlane(this);
        }
    }

    public void OnBox(Box box) => Box = box;

    public void OnBoxNotify(bool active, Box box)
    {
        m_isActive = active;
        Box = active ? box : null;
        m_sprite.color = active ? m_activeColor : Color.white;
    }
}

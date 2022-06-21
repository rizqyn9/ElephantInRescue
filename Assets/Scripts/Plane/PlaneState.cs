using System;
using UnityEngine;

[Serializable]
public class PlaneState<T> where T : Plane
{
    private SpriteRenderer groundRenderer;
    public T Plane { get; private set; }
    public bool IsFocus { get; private set; }
    public Action<bool> OnFocusChange { get; set; }
    public Box BoxSubscribe { get; private set; }

    public PlaneState(T plane, Action<bool> onChange, bool isFocus = false)
    {
        Plane = plane;
        IsFocus = isFocus;
        OnFocusChange = onChange;
        groundRenderer = plane.m_groundRenderer;
    }

    // Pure root without elephant or civilian
    public void SetFocus(bool isFocus, Box box)
    {
        if (Plane.PlaneType != PlaneTypeEnum.ROUTE) return;
        if (IsFocus == isFocus) return;
        if (Plane.Civilian || Plane.Box) isFocus = false;

        IsFocus = isFocus;

        BoxSubscribe = box;

        groundRenderer.color = IsFocus ? new Color(1, 0, 1, .25f) : Color.white;
        OnFocusChange(IsFocus);
    }

    public void OnMouseDown()
    {
        if(IsFocus && BoxSubscribe)
        {
            BoxSubscribe.MoveToPlane(Plane);
            BoxSubscribe = null;    // Unsub
        }
    }

    internal void OnObjectEnter(Collider2D collider) { }

    internal void OnObjectExit(Collider2D collider) { }

    internal void OnCivilianChange(BaseCivilian recent)
    {
        if (recent && BoxSubscribe) BoxSubscribe.Refocus();
    }
}

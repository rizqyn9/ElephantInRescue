using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum InventoryItemType
{
    STONE,
    ROOT,
    KNIFE,
    HAMMER
}

public enum InventoryState
{
    ACTIVE,
    IDDLE,
    DISABLE
}

[RequireComponent(typeof(Button))]
public class InventoryItem : MonoBehaviour
{
    internal Image image;
    internal Button button;
    RectTransform rectTransform;
    [SerializeField] Color32 m_activeColor, m_iddleColor;
    [SerializeField] internal InventoryStateSO inventoryStateSO;
    [SerializeField] InventoryState m_inventoryState = InventoryState.IDDLE;
    public InventoryState InventoryState
    {
        get => m_inventoryState;
        set
        {
            m_inventoryState = value;
            HandleStateOnChange(m_inventoryState, value);
        }
    }

    [SerializeField] InventoryItemType inventoryItemType; 
    public InventoryItemType InventoryItemType { get => inventoryItemType; internal set => inventoryItemType = value; }

    internal virtual void HandleStateOnChange(InventoryState before, InventoryState after)
    {
        if(after == InventoryState.IDDLE)
        {
            if(rectTransform.localScale != Vector3.one)
            {
                LeanTween
                    .scale(rectTransform, Vector3.one, .2f).setEaseInBounce();
            }
            button.interactable = true;
            image.color = m_iddleColor;
        } else if(after == InventoryState.ACTIVE)
        {
            inventoryStateSO.RaiseEvent(this);
            LeanTween
                .scale(rectTransform, rectTransform.localScale * 1.1f, .2f).setEaseInBounce();

            image.color = m_activeColor;
        } else
        {
            button.interactable = false;
        }
    }

    internal virtual void OnEnable()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        inventoryStateSO.OnEventRaised += HandleInventoryEvent;
    }

    internal virtual void OnDisable()
    {
        inventoryStateSO.OnEventRaised -= HandleInventoryEvent;        
    }

    internal virtual void Start()
    {
        InventoryState = m_inventoryState;
    }

    internal virtual void Btn_OnClick()
    {
        switch (InventoryState)
        {
            case InventoryState.ACTIVE:
                InventoryState = InventoryState.IDDLE;
                break;
            case InventoryState.IDDLE:
                InventoryState = InventoryState.ACTIVE;
                break;
            default:
                break;
        }
    }

    internal virtual void HandleInventoryEvent(InventoryItem active)
    {
        if(active && active.InventoryItemType != inventoryItemType && InventoryState == InventoryState.ACTIVE)
        {
            InventoryState = InventoryState.IDDLE;
        }
    }

    internal virtual void Update()
    {
        // TODO refocus when user not touch grid object
        if(InventoryState == InventoryState.ACTIVE)
        {        
            //RaycastHit2D[] hit2Ds = Physics2D.RaycastAll()
        }
    }
}

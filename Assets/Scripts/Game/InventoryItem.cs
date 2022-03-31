using System.Collections;
using System.Collections.Generic;
using EIR.Game;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] Color32 colorActive, colorDisactive;
    [SerializeField] Image image;
    [SerializeField] bool isActive = false;


    [SerializeField] UI_Inventory UI_Inventory => UI_Game.UI_Inventory;
    // Accessor
    public bool IsActive
    {
        get => isActive; set
        {
            if (isActive == value) return;
            isActive = value;
            UpdateState();
        }
    }

    private void Start()
    {
        UpdateState();
    }

    public void On_Click()
    {
        if (UI_Inventory.ActiveItem == gameObject.name) return;
        UI_Game.UI_Inventory.SetInventoryActive(gameObject.name);
    }

    void UpdateState()
    {
        image.color = isActive ? colorActive : colorDisactive;
    }
}

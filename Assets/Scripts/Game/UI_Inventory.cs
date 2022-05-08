using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] GameObject baseInventoryItem;
    [SerializeField] int totalItem = 3;

    [Header("Event")]
    [SerializeField] InventoryStateSO _inventoryChannel = default;

    [Header("Debug")]
    private readonly Dictionary<string, InventoryItem> items = new Dictionary<string, InventoryItem>();
    [SerializeField] string activeItem = string.Empty;

    public string ActiveItem { get => activeItem; }

    private void OnEnable()
    {
        _inventoryChannel.OnEventRaised += HandleInventoryCommand;
    }

    private void OnDisable()
    {
        _inventoryChannel.OnEventRaised -= HandleInventoryCommand;
    }

    private void Start()
    {
        for (int i = 0; i < totalItem; i++)
        {
            GameObject go = Instantiate(baseInventoryItem, transform);
            go.name = go.GetInstanceID().ToString();

            go.GetComponent<RectTransform>().localPosition = new Vector2(i * -60, 0f);

            InventoryItem IE = go.GetComponent<InventoryItem>();
            if (i == 2)
                IE.InventoryItemType = InventoryItemType.TEST2;

            items.Add(go.name, go.GetComponent<InventoryItem>());
        }
    }

    void HandleInventoryCommand(InventoryCommand cmd, InventoryItem item)
    {
        switch (cmd)
        {
            case InventoryCommand.ACTIVE:
                if (!string.IsNullOrEmpty(ActiveItem)) items[ActiveItem].SetDeactive();
                activeItem = item.name;
                items[activeItem].SetActive();
                break;
            case InventoryCommand.DEACTIVE:
                items[activeItem].SetDeactive();
                activeItem = string.Empty;
                break;
            default:
                break;
        }
    }

    public void SetInventoryActive(string key)
    {
        if (activeItem != string.Empty) items[activeItem].IsActive = false;
        items[key].IsActive = true;
        activeItem = key;
    }
}

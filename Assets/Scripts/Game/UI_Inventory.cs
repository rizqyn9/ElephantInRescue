using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] GameObject baseInventoryItem;
    [SerializeField] int totalItem = 3;

    [Header("Debug")]
    private readonly Dictionary<string, InventoryItem> items = new Dictionary<string, InventoryItem>();
    [SerializeField] string activeItem = string.Empty;

    public string ActiveItem { get => activeItem; }

    private void Start()
    {
        for (int i = 0; i < totalItem; i++)
        {
            GameObject go = Instantiate(baseInventoryItem, transform);
            go.name = $"Item-{i}";

            go.GetComponent<RectTransform>().localPosition = new Vector2(i * -60, 0f);

            InventoryItem IE = go.GetComponent<InventoryItem>();

            items.Add(go.name, IE);
        }
    }

    public void SetInventoryActive(string key)
    {
        if (activeItem != string.Empty) items[activeItem].IsActive = false;
        items[key].IsActive = true;
        activeItem = key;
    }
}

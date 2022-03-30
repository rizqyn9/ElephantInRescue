using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] GameObject baseInventoryItem;
    [SerializeField] int totalItem = 3;

    private void Start()
    {
        for(int i = 0; i < totalItem; i++)
        {
            GameObject go = Instantiate(baseInventoryItem, transform);
            go.name = $"Item-{i}";
            go.GetComponent<RectTransform>().localPosition = new Vector2(i * -60, 0f);
        }
    }

}

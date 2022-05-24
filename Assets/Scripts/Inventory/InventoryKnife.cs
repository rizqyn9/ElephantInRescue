using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryKnife : InventoryItem
{
    public override void StartConfiguration()
    {
        InventoryItemType = InventoryItemType.KNIFE;
    }
}

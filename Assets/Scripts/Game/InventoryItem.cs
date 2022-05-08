using EIR.Game;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum InventoryItemType
{
    TEST1,
    TEST2
}

public class InventoryItem : MonoBehaviour
{
    [SerializeField] Color32 colorActive, colorDisactive;
    [SerializeField] Image image;
    [SerializeField] bool _isActive = false;

    [SerializeField] InventoryStateSO _inventoryChannel;

    // Accessor
    public InventoryItemType InventoryItemType = InventoryItemType.TEST1;
    public bool IsActive
    {
        get => _isActive; set
        {
            if (_isActive == value) return;
            _isActive = value;
            UpdateState();
        }
    }

    private void Start()
    {
        UpdateState();
    }

    public void On_Click()
    {
        if (IsActive)
        {
            _inventoryChannel.RaiseEvent(InventoryCommand.DEACTIVE, this);
            SetDeactive();
        }
        else
        {
            _inventoryChannel.RaiseEvent(InventoryCommand.ACTIVE, this);
            SetActive();
        }
    }

    public void SetActive()
    {
        IsActive = true;
    }

    public void SetDeactive()
    {
        IsActive = false;
    }

    void UpdateState()
    {
        image.color = _isActive ? colorActive : colorDisactive;
    }
}

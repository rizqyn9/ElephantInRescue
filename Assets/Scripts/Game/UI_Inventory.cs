using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] List<Transform> m_placedInstance = new List<Transform>();

    [Header("Event")]
    [SerializeField] InventoryStateSO _inventoryChannel = default;
    [SerializeField] GameStateChannelSO m_gameStateChannelSO;

    [Header("Debug")]
    [SerializeField] InventoryItem m_activeInventoryItem;

    private void OnEnable()
    {
        _inventoryChannel.OnEventRaised += HandleInventoryChanged;
        m_gameStateChannelSO.OnEventRaised += HandleGameStateChanged;
    }


    private void OnDisable()
    {
        _inventoryChannel.OnEventRaised -= HandleInventoryChanged;
        m_gameStateChannelSO.OnEventRaised -= HandleGameStateChanged;
    }

    private void HandleInventoryChanged(InventoryItem activeInventory)
    {
        m_activeInventoryItem = activeInventory;
    }

    private void HandleGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.BEFORE_PLAY:
                //InstanceInvetory();
                break;
        }
    }

    private void Start()
    {
        if (!LevelManager.Instance) return;
        if (LevelManager.Instance.GetInventoryGO().Count > m_placedInstance.Count) throw new System.Exception("Please check total intanced inventory");
        for (int i = 0; i < LevelManager.Instance.GetInventoryGO().Count; i++)
        {
            Instantiate(LevelManager.Instance.GetInventoryGO()[i], m_placedInstance[i]);
        }
    }

    //public void SetInventoryActive(string key)
    //{
    //    if (activeItem != string.Empty) items[activeItem].IsActive = false;
    //    items[key].IsActive = true;
    //    activeItem = key;
    //}
}

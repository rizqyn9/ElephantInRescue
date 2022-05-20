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
    private readonly Dictionary<string, InventoryItem> items = new Dictionary<string, InventoryItem>();
    [SerializeField] string activeItem = string.Empty;

    public string ActiveItem { get => activeItem; }

    private void OnEnable()
    {
        //_inventoryChannel.OnEventRaised += HandleInventoryCommand;
        m_gameStateChannelSO.OnEventRaised += HandleGameStateChanged;
    }    

    private void OnDisable()
    {
        //_inventoryChannel.OnEventRaised -= HandleInventoryCommand;
        m_gameStateChannelSO.OnEventRaised -= HandleGameStateChanged;
    }

    //void HandleInventoryCommand(InventoryCommand cmd, InventoryItem item)
    //{
    //    switch (cmd)
    //    {
    //        case InventoryCommand.ACTIVE:
    //            if (!string.IsNullOrEmpty(ActiveItem)) items[ActiveItem].SetDeactive();
    //            activeItem = item.name;
    //            items[activeItem].SetActive();
    //            break;
    //        case InventoryCommand.DEACTIVE:
    //            items[activeItem].SetDeactive();
    //            activeItem = string.Empty;
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private void HandleGameStateChanged(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.BEFORE_PLAY:
                //InstanceInvetory();
                break;
        }
    }

    private void Start()
    {
        if (LevelManager.Instance.GetInventoryGO().Count > m_placedInstance.Count) throw new System.Exception("Please check total intanced inventory");
        for(int i = 0; i < LevelManager.Instance.GetInventoryGO().Count; i++)
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

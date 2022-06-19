using UnityEngine;

public enum PlaneTypeEnum
{
    ROUTE,
    TREE,
    FINISH,
    HOLE
}

[System.Serializable]
[RequireComponent(typeof(BoxCollider2D))]
public class Plane : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] internal InventoryStateSO m_inventoryChannel;

    [SerializeField] bool m_isPlayerInstancePlace = false;

    public PlaneTypeEnum PlaneType { get; internal set; }
    public bool IsPlayerInstancePlace { get => m_isPlayerInstancePlace; }
    public InventoryItem ActiveInventory { get; private set; }

    // Handle State
    public BaseCivilian Civilian { get; private set; }
    public Box Box { get; private set; }
    public PlayerController PlayerController { get; private set; }

    internal virtual void OnEnable()
    {
        m_inventoryChannel.OnEventRaised += HandleInventoryChange;
    }

    internal virtual void OnDisable()
    {
        m_inventoryChannel.OnEventRaised -= HandleInventoryChange;
    }

    internal virtual void Start()
    {
        CheckValidity();
        RegisterPlaneToPlaneManager();
    }

    void CheckValidity()
    {
        if (m_isPlayerInstancePlace && PlaneType != PlaneTypeEnum.ROUTE)
            throw new System.Exception("Instanced place but Plane type not use Route Plane");
    }

    void HandleInventoryChange(InventoryItem activeInventory)
    {
        ActiveInventory = activeInventory;
    }

    public virtual void OnElephant() { }

    public void RegisterPlaneToPlaneManager()
    {
        PlaneManager.AuthorizedPlane(this, out string planeNameHashed);
        gameObject.name = planeNameHashed;
    }

    internal virtual void OnMouseDown() { }

    internal virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) OnElephant();
    }

    internal void OnTriggerStay2D(Collider2D collision) { }

    internal void OnTriggerExit2D(Collider2D collision) { }

    /// <summary>
    /// Trigger every Civilian property has changed
    /// </summary>
    /// <param name="civilian">Can be null</param>
    public virtual void SetCivilian(BaseCivilian civilian) =>
        Civilian = civilian;

    /// <summary>
    /// Trigger every Box property has changed
    /// </summary>
    /// <param name="box"></param>
    public void SetBox(Box box) => Box = box;

    /// <summary>
    /// Trigger every Player property has changed
    /// </summary>
    /// <param name="Player"></param>
    public void SetPlayer(PlayerController player) => PlayerController = player;

    public bool IsFocus { get; private set; }
    public void SetFocus(bool isFocus, Box box)
    {
        IsFocus = isFocus;
        SetBox(box);
        OnFocusChanged();
    }

    public void SetFocus(bool isFocus)
    {
        IsFocus = isFocus;
        OnFocusChanged();
    }

    internal virtual void OnFocusChanged() { }
}

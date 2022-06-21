using UnityEngine;

public enum PlaneTypeEnum
{
    ROUTE,
    TREE,
    FINISH,
    HOLE,
    OBSTACLE
}

[System.Serializable]
[RequireComponent(typeof(BoxCollider2D))]
public class Plane : MonoBehaviour
{
    [SerializeField] internal SpriteRenderer m_groundRenderer;

    [Header("Events")]
    [SerializeField] internal InventoryStateSO m_inventoryChannel;
    [SerializeField] bool m_isPlayerInstancePlace = false;

    public PlaneTypeEnum PlaneType { get; private set; }
    public bool IsPlayerInstancePlace { get => m_isPlayerInstancePlace; }
    public InventoryItem ActiveInventory { get; private set; }
    [SerializeField] public PlaneState<Plane> PlaneState { get; set; }

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

    /// <summary>
    /// Ensure set PlaneType before base.Start was invoke
    /// </summary>
    internal virtual void Start()
    {
        PlaneState = new PlaneState<Plane>(this, HandlePlaneFocusOnChange);
        CheckValidity();
        PlaneManager.AuthorizedPlane(this, out string planeNameHashed);
    }

    internal void SetPlaneType(PlaneTypeEnum planeType) =>
        PlaneType = planeType;

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

    internal virtual void OnMouseDown() {
        PlaneState.OnMouseDown();
    }

    internal virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) OnElephant();
        else
            OnObjectEnter(collision);
    }

    internal virtual void OnObjectEnter(Collider2D collider)
    {
        PlaneState.OnObjectEnter(collider);
    }

    internal void OnTriggerStay2D(Collider2D collision) { }

    internal void OnTriggerExit2D(Collider2D collision) {
        OnObjectExit(collision);
    }

    internal virtual void OnObjectExit(Collider2D collider)
    {
        PlaneState.OnObjectExit(collider);
    }

    #region Plane State
    public virtual void SetCivilian(BaseCivilian civilian) => OnCivilianChange(civilian);
    internal virtual void OnCivilianChange(BaseCivilian civilian)
    {
        Civilian = civilian;    // Setter
        PlaneState.OnCivilianChange(civilian);
    }

    public virtual void SetBox(Box box) => OnBoxChange(box);
    internal virtual void OnBoxChange(Box box)
    {
        Box = box;
    }
    public void SetPlayer(PlayerController player) => OnPlayerChange(player);
    internal virtual void OnPlayerChange(PlayerController player)
    {
        PlayerController = player;
    }

    public virtual void HandlePlaneFocusOnChange(bool isFocus) { }
    #endregion

}

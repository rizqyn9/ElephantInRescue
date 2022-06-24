using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController m_instance;
    public static PlayerController Instance { get => m_instance; }

    [SerializeField] Plane m_planeStartPosition;
    [SerializeField] SpriteRenderer m_spriteRenderer;

    [Header("Events")]
    public GameStateChannelSO m_gameStateChannel;
    public InventoryStateSO m_inventoryStateSO;

    public Plane CurrentPlane { get; private set; }
    public ElephantAnimation ElephantAnimation { get; internal set; }
    public bool IsDead { get; private set; }
    public bool CanMove { get; internal set; }
    public Vector3 Direction { get; internal set; }
    public GameObject RenderObject { get => m_spriteRenderer.gameObject; }

    private void OnEnable()
    {
        m_gameStateChannel.OnEventRaised += HandleGameState;
        m_inventoryStateSO.OnEventRaised += HandleInventoryState;
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        ElephantAnimation = GetComponentInChildren<ElephantAnimation>();
    }

    private void OnDisable()
    {
        m_gameStateChannel.OnEventRaised -= HandleGameState;
        m_inventoryStateSO.OnEventRaised -= HandleInventoryState;
    }

    private void Start()
    {
        m_spriteRenderer.enabled = false;
        CanMove = false;
        IsDead = false;
        transform.position = m_planeStartPosition.transform.position;
        SetPlane(m_planeStartPosition);
    }

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else Destroy(gameObject);
    }

    private void OnDestroy()
    {
        m_instance = null;
    }

    void HandleGameState(GameState before, GameState gameState)
    {
        switch (gameState)
        {
            case GameState.PLAY:
                if (before == GameState.PAUSE)
                    CanMove = true;
                else 
                    InitializePlayer();
                break;
            case GameState.BEFORE_PLAY:
            case GameState.PAUSE:
            case GameState.FINISH:
                CanMove = false;
                break;
        }
    }

    public void OnHitCivilian(Civilian civilian)
    {
        IsDead = true;
        ElephantAnimation.Knock();
        LeanTween.value(0, 1, 5f).setOnComplete(LevelManager.Instance.LoseCondition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    void HandleInventoryState(InventoryItem activeInventory) { }

    void InitializePlayer()
    {
        m_spriteRenderer.enabled = true;
        CanMove = true;
    }

    public void SetDirection(Vector3 dir)
    {
        if (!CanMove || IsDead) return;

        Direction = dir;

        RaycastHit2D[] raycast = Physics2D.RaycastAll(transform.position, dir, 1f);

        foreach (RaycastHit2D ray in raycast)
        {
            if (ray.collider != null)
            {
                Plane target = ray.collider.GetComponent<Plane>();
                if (target)
                {
                    if (PlayerUtils.ShouldMove(target))
                    {
                        if (target.name == CurrentPlane?.name) continue;
                        MoveTowards(ray.collider, target);
                        SetPlane(target);
                        break;
                    }
                }
            }
        }
    }

    void SetPlane(Plane plane)
    {
        // Unsub older plane
        CurrentPlane?.SetPlayer(null);

        CurrentPlane = plane;
        CurrentPlane.SetPlayer(this);
    }

    void MoveTowards(Collider2D collider, Plane plane)
    {
        LeanTween
            .move(gameObject, collider.bounds.center, .5f)
            .setOnStart(() =>
            {
                CanMove = false;
                ElephantAnimation.Walk();
            })
            .setOnComplete(() =>
            {
                ElephantAnimation.Iddle();
                CanMove = true;
                plane?.OnElephant();
            });
    }

    void StopAllTweening() => LeanTween.cancel(gameObject);
}

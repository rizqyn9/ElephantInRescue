using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController m_instance;
    public static PlayerController Instance { get => m_instance; }

    public Plane PlanePosition { get; private set; }
    public bool IsDead { get; private set; }

    [SerializeField] Plane m_planeStartPosition;
    [SerializeField] ElephantAnimation m_elephantAnimation;
    SpriteRenderer m_spriteRenderer;
    GameObject m_player;
    [HideInInspector] [SerializeField] bool m_canMove;
    Vector3 m_direction;

    [Header("Events")]
    public GameStateChannelSO m_gameStateChannel;
    public InventoryStateSO m_inventoryStateSO;

    // Get Instances object
    public UI_Game UI_Game => UI_Game.Instance;

    private void OnEnable()
    {
        m_gameStateChannel.OnEventRaised += HandleGameState;
        m_inventoryStateSO.OnEventRaised += HandleInventoryState;
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_player = m_spriteRenderer.gameObject;
        m_elephantAnimation = GetComponentInChildren<ElephantAnimation>();
    }

    private void OnDisable()
    {
        m_gameStateChannel.OnEventRaised -= HandleGameState;
        m_inventoryStateSO.OnEventRaised -= HandleInventoryState;
    }

    private void Start()
    {
        m_spriteRenderer.enabled = false;
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
                    m_canMove = true;
                else 
                    InitializePlayer();
                break;
            case GameState.BEFORE_PLAY:
            case GameState.PAUSE:
            case GameState.FINISH:
                m_canMove = false;
                break;
        }
    }

    public void OnHitCivilian(BaseCivilian civilian)
    {
        IsDead = true;
        m_elephantAnimation.Knock(m_direction);
        LeanTween.value(0, 1, 5f).setOnComplete(LevelManager.Instance.LoseCondition);
    }

    void HandleInventoryState(InventoryItem activeInventory)
    {

    }

    void InitializePlayer()
    {
        m_canMove = true;
        IsDead = false;
        gameObject.transform.position = m_planeStartPosition.transform.position;
        m_spriteRenderer.enabled = true;
    }

    [SerializeField] Plane target;
    public void SetDirection(Vector3 dir)
    {
        if (!m_canMove || IsDead) return;

        m_direction = dir;
        RaycastHit2D[] raycast = Physics2D.RaycastAll(transform.position, dir, 1f);

        foreach (RaycastHit2D ray in raycast)
        {
            if (ray.collider != null)
            {
                target = ray.collider.GetComponent<Plane>();
                if (target)
                {
                    if (target.PlaneType == PlaneTypeEnum.ROUTE
                        || target.PlaneType == PlaneTypeEnum.FINISH
                        || (target.PlaneType == PlaneTypeEnum.TREE && (target as PlaneTree).Destroyed)
                    )
                    {
                        if (target.name == PlanePosition?.name) continue;
                        LeanTween
                            .move(gameObject, ray.collider.bounds.center, .5f)
                            .setOnStart(() =>
                            {
                                m_canMove = false;
                                m_elephantAnimation.Walk(m_direction);
                            })
                            .setOnComplete(() =>
                            {
                                m_elephantAnimation.Iddle(m_direction);
                                m_canMove = true;
                                target?.OnElephant();
                                PlanePosition = target;
                            });
                    } else
                    {
                        continue;
                    }
                }
            }
        }
    }
}

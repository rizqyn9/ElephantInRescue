using UnityEngine;

namespace EIR.Game
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController m_instance;
        public static PlayerController Instance { get => m_instance; }
        [SerializeField] SpriteRenderer m_spriteRenderer;
        [SerializeField] Vector3 m_direction;
        [HideInInspector][SerializeField] bool m_canMove;
        public Plane PlanePosition { get; private set; }

        [SerializeField] Plane m_planeStartPosition;

        [Header("Events")]
        public GameStateChannelSO m_gameStateChannel;
        public InventoryStateSO m_inventoryStateSO;

        // Get Instances object
        public UI_Game UI_Game => UI_Game.Instance;

        private void OnEnable()
        {
            m_gameStateChannel.OnEventRaised += HandleGameState;
            m_inventoryStateSO.OnEventRaised += HandleInventoryState;
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

        void HandleGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.PLAY:
                    InitializePlayer();
                    break;
                case GameState.BEFORE_PLAY:
                case GameState.PAUSE:
                case GameState.FINISH:
                case GameState.TIME_OUT:
                    m_canMove = false;
                    break;
            }       
        }

        public void OnHitCivilian(BaseCivilian civilian)
        {
            Destroy(gameObject);
            civilian.CivilianWalk.Stop();
        }

        void HandleInventoryState(InventoryItem activeInventory)
        {

        }

        void InitializePlayer()
        {
            m_canMove = true;
            gameObject.transform.position = m_planeStartPosition.transform.position;
            m_spriteRenderer.enabled = true;
        }

        public void SetDirection(Vector3 dir)
        {
            if (!m_canMove) return;

            m_direction = dir;
            RaycastHit2D[] raycast = Physics2D.RaycastAll(transform.position, dir, 1f);

            
            foreach (RaycastHit2D ray in raycast)
            {
                if (ray.collider != null)
                {
                    Plane target = ray.collider.GetComponent<Plane>();
                    if (target)
                    {
                        if (target.PlaneType == PlaneTypeEnum.ROUTE || target.PlaneType == PlaneTypeEnum.FINISH)
                        {
                            transform.position = ray.transform.position;
                            PlanePosition = target;
                            target.OnElephant();
                        }
                    }
                }
            }
        }
    }
}

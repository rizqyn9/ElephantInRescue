using UnityEngine;

namespace EIR.Game
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController m_instance;
        public static PlayerController Instance { get => m_instance; }
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Vector3 direction;
        public Plane PlanePosition { get; private set; }

        [SerializeField] Plane m_planeStartPosition;

        [Header("Events")]
        public GameStateChannelSO gameStateChannel;
        public InventoryStateSO inventoryStateSO;

        // Get Instances object
        public UI_Game UI_Game => UI_Game.Instance;

        private void OnEnable()
        {
            gameStateChannel.OnEventRaised += HandleGameState;
            inventoryStateSO.OnEventRaised += HandleInventoryState;
        }

        private void OnDisable()
        {
            gameStateChannel.OnEventRaised -= HandleGameState;            
            inventoryStateSO.OnEventRaised -= HandleInventoryState;
        }

        private void Start()
        {
            spriteRenderer.enabled = false;
        }

        private void Awake()
        {
            if (m_instance == null)
                m_instance = this;
            else Destroy(gameObject);
        }

        void HandleGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.PLAY:
                    InitializePlayer();
                    break;
            }                
        }

        void HandleInventoryState(InventoryItem activeInventory)
        {

        }

        public void InitializePlayer()
        {
            //if (PlaneManager.Instance.PlayerPlaneInstance)
            //{
            //    transform.position = PlaneManager.Instance.PlayerPlaneInstance.gameObject.transform.position;
            //}
            //else throw new System.Exception("No placed for instance elephant");
            gameObject.transform.position = m_planeStartPosition.transform.position;
            spriteRenderer.enabled = true;
        }

        public void SetDirection(Vector3 dir)
        {
            direction = dir;
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

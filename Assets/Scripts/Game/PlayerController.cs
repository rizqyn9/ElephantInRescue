using UnityEngine;

namespace EIR.Game
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance { get => _instance; }
        [SerializeField] Vector3 direction;

        [Header("Events")]
        public GameStateChannelSO gameStateChannel;
        public InventoryStateSO inventoryStateSO;

        // Get Instances object
        public UI_Game UI_Game => UI_Game.Instance;

        private void OnEnable()
        {
            gameStateChannel.OnEventRaised += handleGameState;
            inventoryStateSO.OnEventRaised += handleInventoryState;
        }

        private void OnDisable()
        {
            gameStateChannel.OnEventRaised -= handleGameState;            
            inventoryStateSO.OnEventRaised -= handleInventoryState;
        }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else Destroy(gameObject);
        }

        void handleGameState(GameState _gameState)
        {

        }

        void handleInventoryState(InventoryCommand _cmd, InventoryItem _item)
        {

        }

        private void Start()
        {
            gameObject.LeanAlpha(0, 0);    
        }

        public void InitializePlayer()
        {
            gameObject.LeanAlpha(1, 2);
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
                        if (target.PlaneType == PlaneType.ROUTE)
                        {
                            transform.position = ray.transform.position;
                        }
                    }
                }
            }
        }
    }
}

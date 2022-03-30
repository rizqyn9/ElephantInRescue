using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR.Game
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance { get => _instance; }
        [SerializeField] Vector3 direction;

        // Get Instances object
        public UI_Game UI_Game => UI_Game.Instance;
        

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="dir"></param>
        public void SetDirection(Vector3 dir)
        {
            
            direction = dir;
            RaycastHit2D[] raycast = Physics2D.RaycastAll(transform.position, dir,1f);

            foreach (RaycastHit2D ray in raycast)
            {
                if(ray.collider != null)
                {
                    print(ray.collider.gameObject.name);
                    transform.position = ray.transform.position;
                }
            }
        }
    }
}

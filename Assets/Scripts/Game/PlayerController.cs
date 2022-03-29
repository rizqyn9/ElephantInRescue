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

        public void SetDirection(Vector3 dir)
        {
            //UI_Game.directionText.text = dir;
            direction = dir;
        }
    }
}

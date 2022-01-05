using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR.Game
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager _instance;
        public static LevelManager Instance { get => _instance; }

        [Header("Properties")]
        [SerializeField] UI_Game ui_Game;

        [Header("Debug")]
        public LevelBase levelBase;


        // Accessor
        public static UI_Game UI_Game => Instance.ui_Game;


        private void Awake()
        {
            if (_instance == null) _instance = this;
        }

        public void Init(LevelBase _levelBase)
        {
            levelBase = _levelBase;

            UI_Game.Init();
        }
    }
}

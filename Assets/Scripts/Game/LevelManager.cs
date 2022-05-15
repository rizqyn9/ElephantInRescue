using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace EIR.Game
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager _instance;
        public static LevelManager Instance { get => _instance; }

        [Header("Event")]
        [SerializeField] GameStateChannelSO gameStateChannel = default;

        [Header("Debug")]
        public LevelBase levelBase;

        private void OnEnable()
        {
            gameStateChannel.OnEventRaised += HandleGameStateChange;
        }

        private void OnDisable()
        {
            gameStateChannel.OnEventRaised -= HandleGameStateChange;
        }

        [SerializeField] GameState m_gameState;
        void HandleGameStateChange(GameState _gameState)
        {
            m_gameState = _gameState;
        }


        private void Awake()
        {
            if (_instance == null) _instance = this;
        }

        void Start()
        {
            StartCoroutine(StartEnumerator());
        }

        IEnumerator StartEnumerator()
        {
            /**
             * Start the game after all animation have done
             */
            Camera.main.transform.DOMoveZ(Camera.main.transform.position.z, 3).SetEase(Ease.InOutQuart).From(0).OnComplete(() =>
            {
                gameStateChannel.RaiseEvent(GameState.PLAY);
                PlayerController.Instance.InitializePlayer();
            });

            yield return 1;
        }

        public void WinCondition()
        {
            gameStateChannel.RaiseEvent(GameState.FINISH);
        }

        public void LoseCondition()
        {
            gameStateChannel.RaiseEvent(GameState.FINISH);
        }
    }
}

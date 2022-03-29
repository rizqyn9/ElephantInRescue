using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EIR.Game
{
    public class UI_Game : MonoBehaviour
    {
        private static UI_Game _instance;
        public static UI_Game Instance { get => _instance; }

        [Header("Properties")]
        public TMP_Text profileLevel;
        public TMP_Text profileStage;
        public TMP_Text directionText;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public void Init()
        {
            UpdateCompProfile();
        }

        public void UpdateCompProfile()
        {
            profileLevel.text = $"Level : {LevelManager.Instance.levelBase.level}";
            profileStage.text = $"Stage : {LevelManager.Instance.levelBase.stages.Count}";
        }

        public void UpdateUI() { }
    }
}

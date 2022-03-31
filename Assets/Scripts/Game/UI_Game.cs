using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        [Header("Pause")]
        [SerializeField] Button btnPause;
        [SerializeField] GameObject pauseModal;
        [SerializeField] UI_Inventory uI_Inventory;

        public static UI_Inventory UI_Inventory => Instance.uI_Inventory;

        private void Awake()
        {
            if (_instance == null) _instance = this;
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

        public void Btn_Pause()
        {

        }
    }
}

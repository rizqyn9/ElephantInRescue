using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EIR.Game
{
    public class UI_Game : MonoBehaviour
    {
        [Header("Properties")]
        public TMP_Text profileLevel;
        public TMP_Text profileStage;

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

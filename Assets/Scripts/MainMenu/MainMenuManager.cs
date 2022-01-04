using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR
{
    public class MainMenuManager : MonoBehaviour
    {
        private static MainMenuManager _instance;
        public static MainMenuManager Instance { get => _instance; }

        [Header("Properties")]
        public UI_MainMenu ui_MainMenu;

        // Accessor
        public static UI_MainMenu UI_MainMenu => Instance.ui_MainMenu;

        private void Awake()
        {
            if(_instance == null)   // Singleton
                _instance = this;
        }

        public void spawnForm() => ui_MainMenu.spawnForm();

        public void Init()
        {

        }

        public void formSuccess()
        {
            print("Success");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR
{
    public class MainMenuManager : MonoBehaviour
    {
        private static MainMenuManager _instance;
        public static MainMenuManager Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void init()
        {

        }

        public void formSuccess()
        {

        }
    }
}

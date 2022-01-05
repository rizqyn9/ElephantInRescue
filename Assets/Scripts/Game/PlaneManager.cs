using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR.Game
{
    public class PlaneManager : MonoBehaviour
    {
        private static PlaneManager _instance;
        public static PlaneManager Instance;

        private void Awake()
        {
            if (_instance == null) _instance = this;
            else Destroy(gameObject);
        }

    }
}

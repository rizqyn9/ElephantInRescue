using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR.Game
{
    public class Plane : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] short index;

        public void Init(short _index)
        {
            index = _index;
        }
    }
}

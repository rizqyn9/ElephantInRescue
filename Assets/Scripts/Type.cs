using System;
using UnityEngine;



namespace EIR
{
    [Serializable]
    public struct UserModel
    {
        public string userName;
        public int currentLevel;
        public int currentStage;
        public object userPreferances;
    }
}
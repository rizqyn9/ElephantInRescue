using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR
{
    public class UI_MainMenu : MonoBehaviour
    {
        [Header("Properties")]
        public UI_Form ui_Form;

        private void Start()
        {
            ui_Form.gameObject.SetActive(false);
        }
    }
}

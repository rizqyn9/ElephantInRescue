using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EIR
{
    public class UI_MainMenu : MonoBehaviour
    {
        [Header("Properties")]
        public UI_Form ui_Form;
        public TMP_Text userName;

        private void Start()
        {
            ui_Form.gameObject.SetActive(false);
        }

        public void spawnForm() => 
            ui_Form.gameObject.SetActive(true);

        public void setUserName(string _username) => userName.text = _username;
    }
}

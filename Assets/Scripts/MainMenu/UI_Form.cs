using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using EIR.MainMenu;
using EIR.Game;

namespace EIR
{
    public class UI_Form : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] TMP_InputField input;
        [SerializeField] Button btn_Submit;
        [SerializeField] TMP_Text textError;

        string pattern = @"[\w*\d\s]";

        #region Test
        private void OnEnable()
        {
            //btn_Submit.interactable = true;
            //input.onValueChanged.AddListener(validateInput);
        }

        private void OnDisable()
        {
            //input.onValueChanged.RemoveListener(validateInput);
        }
        #endregion

        public void Btn_Submit() =>
            validateInput(input.text.TrimStart().TrimEnd());

        void validateInput(string _input)
        {
            MatchCollection match = new Regex(pattern).Matches(_input);

            if (match.Count != _input.Length)
                textError.text = "Input not assignable no symbols";
            else if (_input.Length >= 8 && _input.Length <= 4)
                textError.text = "Input length min 4 max 8";
            else
            {
                textError.text = "";
                print("OK");
                GameManager.Instance.playerDataModel.userName = _input;
                MainMenuManager.Instance.formSuccess();
            }
        }
    }
}

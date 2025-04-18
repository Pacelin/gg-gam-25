using TMPro;
using UnityEngine;

namespace TSS.Core.UI
{
    public class InputFieldAllowedCharacters : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField, TextArea]
        private string _allowedCharacters;

        private void OnEnable()
        {
            _inputField.onValidateInput += OnValidateInput;
        }

        private void OnDisable()
        {
            _inputField.onValidateInput -= OnValidateInput;
        }

        private char OnValidateInput(string text, int charIndex, char addedChar)
        {
            if (_allowedCharacters.Contains(addedChar))
                return addedChar;

            return '\0';
        }
    }
}
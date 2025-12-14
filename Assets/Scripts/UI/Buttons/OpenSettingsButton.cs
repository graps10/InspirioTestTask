using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class OpenSettingsButton : MonoBehaviour
    {
        private Button _btn;

        private void Awake() => _btn = GetComponent<Button>();

        private void OnEnable() => _btn.onClick.AddListener(OpenSettings);
        private void OnDisable() => _btn.onClick.RemoveListener(OpenSettings);

        private void OpenSettings() => PopupManager.Instance.ShowSettings();
    }
}
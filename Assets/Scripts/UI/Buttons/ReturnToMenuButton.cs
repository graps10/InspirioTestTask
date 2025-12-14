using SceneControl;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ReturnToMenuButton : MonoBehaviour
    {
        [Header("Settings")] [Tooltip("Exact name of the Main Menu scene")] 
        [SerializeField] private string menuSceneName = "MainMenu";

        private Button _button;

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(GoToMenu);
        private void OnDisable() => _button.onClick.RemoveListener(GoToMenu);

        private void GoToMenu() => SceneLoader.Instance.LoadScene(menuSceneName);
    }
}
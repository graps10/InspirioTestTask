using DG.Tweening;
using SceneControl;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        private const float Button_Anim_Duration = 0.2f;
        private const float Button_End_Scale = 1f;
        private const Ease Button_Ease_Type = Ease.OutBack;
        
        [Header("Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button workoutButton;
        [SerializeField] private Button awardsButton;
        [SerializeField] private Button boardButton;
        [SerializeField] private Button settingsButton;

        private void OnEnable()
        {
            playButton.onClick.AddListener(() => LoadLevel("SlotsScene"));
            workoutButton.onClick.AddListener(() => LoadLevel("WorkoutScene"));
            awardsButton.onClick.AddListener(() => LoadLevel("AwardsScene"));
            boardButton.onClick.AddListener(() => LoadLevel("BoardScene"));
            
            // TODO: settings button opens settings popup
            
            AnimateButtonsEntrance();
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
            workoutButton.onClick.RemoveAllListeners();
            awardsButton.onClick.RemoveAllListeners();
            boardButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
        }

        private void LoadLevel(string sceneName)
        {
            SceneLoader.Instance.LoadScene(sceneName);
        }

        private void AnimateButtonsEntrance()
        {
            playButton.transform.localScale = Vector3.zero;
            workoutButton.transform.localScale = Vector3.zero;
            awardsButton.transform.localScale = Vector3.zero;
            boardButton.transform.localScale = Vector3.zero;
            settingsButton.transform.localScale = Vector3.zero;
            
            Sequence seq = DOTween.Sequence();
            seq.Append(playButton.transform.DOScale(Button_End_Scale, Button_Anim_Duration).SetEase(Button_Ease_Type));
            seq.Append(workoutButton.transform.DOScale(Button_End_Scale, Button_Anim_Duration).SetEase(Button_Ease_Type));
            seq.Append(awardsButton.transform.DOScale(Button_End_Scale, Button_Anim_Duration).SetEase(Button_Ease_Type));
            seq.Append(boardButton.transform.DOScale(Button_End_Scale, Button_Anim_Duration).SetEase(Button_Ease_Type));
            seq.Append(settingsButton.transform.DOScale(Button_End_Scale, Button_Anim_Duration).SetEase(Button_Ease_Type));
        }
    }
}
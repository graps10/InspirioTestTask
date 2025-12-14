using Audio;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Popups
{
    public class SettingsPopup : PopupBase
    {
        private const float Volume_Threshold = 0.001f;
            
        [Header("Audio Sliders")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider soundSlider;
        
        [Header("Icons")]
        [SerializeField] private Image musicIcon;
        [SerializeField] private Image soundIcon;
        [SerializeField] private Sprite musicOnSprite;
        [SerializeField] private Sprite musicOffSprite;
        [SerializeField] private Sprite soundOnSprite;
        [SerializeField] private Sprite soundOffSprite;

        [Header("Vibration")]
        [SerializeField] private Button vibrationToggleBtn;
        [SerializeField] private GameObject vibrationOnObj;
        [SerializeField] private GameObject vibrationOffObj;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            LoadState();
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
            soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
            vibrationToggleBtn.onClick.AddListener(OnVibrationToggle);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
            soundSlider.onValueChanged.RemoveListener(OnSoundSliderChanged);
            vibrationToggleBtn.onClick.RemoveListener(OnVibrationToggle);
        }

        private void LoadState()
        {
            UpdateVibrationUI(GameDataManager.IsVibrationOn);
            
            float musicVol = GameDataManager.MusicVolume;
            float soundVol = GameDataManager.SoundVolume;

            musicSlider.value = musicVol;
            soundSlider.value = soundVol;
            
            UpdateMusicIcon(musicVol);
            UpdateSoundIcon(soundVol);
        }

        private void OnMusicSliderChanged(float value)
        {
            AudioManager.Instance.SetMusicVolume(value);
            UpdateMusicIcon(value);
        }

        private void OnSoundSliderChanged(float value)
        {
            AudioManager.Instance.SetSFXVolume(value);
            UpdateSoundIcon(value);
        }

        private void OnVibrationToggle()
        {
            bool newState = !GameDataManager.IsVibrationOn;
            GameDataManager.IsVibrationOn = newState;
            UpdateVibrationUI(newState);
            
            if(newState) VibrationManager.Vibrate();
        }

        private void UpdateMusicIcon(float value)
        {
            if (musicIcon)
                musicIcon.sprite = (value > Volume_Threshold) ? musicOnSprite : musicOffSprite;
        }

        private void UpdateSoundIcon(float value)
        {
            if (soundIcon)
                soundIcon.sprite = (value > Volume_Threshold) ? soundOnSprite : soundOffSprite;
        }

        private void UpdateVibrationUI(bool isOn)
        {
            if(vibrationOnObj) vibrationOnObj.SetActive(isOn);
            if(vibrationOffObj) vibrationOffObj.SetActive(!isOn);
        }
    }
}
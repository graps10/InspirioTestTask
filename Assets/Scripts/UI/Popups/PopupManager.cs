using UnityEngine;
using Utils;

namespace UI.Popups
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager Instance;

        [Header("Popups References")]
        [SerializeField] private RewardPopup slotsWinPopup;
        [SerializeField] private RewardPopup workoutWinPopup;
        [SerializeField] private SettingsPopup settingsPopup;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        
        public void ShowSlotWin(int amount)
        {
            if (slotsWinPopup == null) 
            {
                Debug.LogError("Slots Win Popup is not assigned!");
                return;
            }
            
            slotsWinPopup.Setup(amount);
            VibrationManager.Vibrate();
        }

        public void ShowWorkoutComplete(int amount)
        {
            if (workoutWinPopup == null) 
            {
                Debug.LogError("Workout Win Popup is not assigned!");
                return;
            }
            
            workoutWinPopup.Setup(amount);
            VibrationManager.Vibrate();
        }

        public void ShowSettings()
        {
            if (settingsPopup == null) 
            {
                Debug.LogError("Settings Popup is not assigned!");
                return;
            }
            
            settingsPopup.Show();
        }
    }
}
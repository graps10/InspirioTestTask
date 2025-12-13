using UnityEngine;

namespace UI.Popups
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager Instance;

        [Header("Popups References")]
        [SerializeField] private RewardPopup slotsWinPopup;
        [SerializeField] private RewardPopup workoutWinPopup;

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
        }

        public void ShowWorkoutComplete(int amount)
        {
            if (workoutWinPopup == null) 
            {
                Debug.LogError("Workout Win Popup is not assigned!");
                return;
            }
            
            workoutWinPopup.Setup(amount);
        }
    }
}
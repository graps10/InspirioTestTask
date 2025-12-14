using System.Collections;
using Audio;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Workout
{
    public class WorkoutController : MonoBehaviour
    {
        private const int Seconds_Per_Minute = 60;
        
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Button startButton;

        [Header("Settings")]
        [Tooltip("Duration in seconds (e.g., 300 for 5 minutes)")]
        [SerializeField] private float workoutDuration = 10f;
        [SerializeField] private int rewardAmount = 300;

        private float _currentTime;
        private bool _isRunning;
        private Coroutine _timerCoroutine;

        private void OnEnable()
        {
            startButton.onClick.AddListener(StartWorkout);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(StartWorkout);
            StopWorkout();
        }
        
        private void Start() => ResetTimerUI();

        private void StartWorkout()
        {
            if (_isRunning) return;

            _isRunning = true;
            _currentTime = workoutDuration;
            startButton.interactable = false;

            _timerCoroutine = StartCoroutine(TimerRoutine());
        }

        private void StopWorkout()
        {
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
            
            _isRunning = false;
            if(startButton != null) startButton.interactable = true;
            ResetTimerUI();
        }

        private void ResetTimerUI() => UpdateTimerText(workoutDuration);

        private IEnumerator TimerRoutine()
        {
            float endTime = Time.time + workoutDuration;
    
            while (Time.time < endTime)
            {
                float remaining = endTime - Time.time;
                UpdateTimerText(remaining);
                yield return null;
            }
    
            UpdateTimerText(0);
            FinishWorkout();
        }

        private void UpdateTimerText(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / Seconds_Per_Minute);
            int seconds = Mathf.FloorToInt(timeInSeconds % Seconds_Per_Minute);
            
            if(timerText != null)
                timerText.text = $"{minutes:00}:{seconds:00}";
        }

        private void FinishWorkout()
        {
            _isRunning = false;
            startButton.interactable = true;
            
            AudioManager.Instance.PlaySound(SoundType.WorkoutComplete);
            
            PopupManager.Instance.ShowWorkoutComplete(rewardAmount);
            CurrencyManager.AddCoins(rewardAmount);
            
            GameDataManager.IncrementWorkouts();
            
            ResetTimerUI();
            
            //Debug.Log($"Workout Finished! Reward: {rewardAmount}");
        }
    }
}
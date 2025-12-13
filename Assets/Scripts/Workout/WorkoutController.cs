using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Workout
{
    public class WorkoutController : MonoBehaviour
    {
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
            startButton.interactable = true;
            ResetTimerUI();
        }

        private void ResetTimerUI() => UpdateTimerText(workoutDuration);

        private IEnumerator TimerRoutine()
        {
            UpdateTimerText(_currentTime);

            while (_currentTime > 0)
            {
                yield return new WaitForSeconds(1f);
                
                _currentTime--;
                UpdateTimerText(_currentTime);
            }

            FinishWorkout();
        }

        private void UpdateTimerText(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60);
            
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        private void FinishWorkout()
        {
            _isRunning = false;
            startButton.interactable = true;
            
            Debug.Log($"Workout Finished! Reward: {rewardAmount}");
            CurrencyManager.AddCoins(rewardAmount);
            
            ResetTimerUI();
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Slots
{
    public class SlotMachineController : MonoBehaviour
    {
        [SerializeField] private SlotReel[] reels;
        [SerializeField] private Button spinButton;
    
        [Header("Settings")]
        [SerializeField] private int symbolCount = 6;
        [SerializeField] private float spinDuration = 2.0f;
        [SerializeField] private float reelDelay = 0.3f;
        [Tooltip("Additional delay after spin finishes to show win popup (accounts for landing animation)")]
        [SerializeField] private float winCheckDelay = 0.5f;
    
        [Header("Rewards")]
        [SerializeField] private int smallWinAmount = 100;
        [SerializeField] private int bigWinAmount = 300;
        
        private bool _isSpinning;
        private int[] _currentResult;

        private void OnEnable() => spinButton.onClick.AddListener(StartSpin);

        private void OnDisable() => spinButton.onClick.RemoveListener(StartSpin);

        private void Start()
        {
            foreach(var reel in reels) 
                reel.Initialize(symbolCount);
        }

        private void StartSpin()
        {
            if (_isSpinning) return;
            
            _isSpinning = true;
            spinButton.interactable = false;
            
            _currentResult = GenerateRandomResult();
            
            Debug.Log($"Spin Result: [{string.Join(", ", _currentResult)}]");
            
            for (int i = 0; i < reels.Length; i++)
                reels[i].Spin(spinDuration, _currentResult[i], i * reelDelay);
            
            float totalWaitTime = spinDuration + (reels.Length * reelDelay) + winCheckDelay;
            StartCoroutine(WaitForSpinComplete(totalWaitTime));
        }
        
        private int[] GenerateRandomResult()
        {
            int[] result = new int[reels.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = Random.Range(0, symbolCount);
           
            return result;
        }

        private IEnumerator WaitForSpinComplete(float delay)
        {
            yield return new WaitForSeconds(delay);
            ProcessWin();
            
            _isSpinning = false;
            spinButton.interactable = true;
        }

        private void ProcessWin()
        {
            if (IsJackpot(_currentResult))
            {
                Debug.Log($"BIG WIN! +{bigWinAmount}");
                CurrencyManager.AddCoins(bigWinAmount);
            }
            else if (IsSmallWin(_currentResult))
            {
                Debug.Log($"Small Win! +{smallWinAmount}");
                CurrencyManager.AddCoins(smallWinAmount);
            }
            else
                Debug.Log("No luck this time.");
        }
        
        private bool IsJackpot(int[] results)
        {
            return results[0] == results[1] && results[1] == results[2];
        }

        private bool IsSmallWin(int[] results)
        {
            return results[0] == results[1] || 
                   results[1] == results[2] || 
                   results[0] == results[2];
        }
    }
}

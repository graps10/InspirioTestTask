using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Slots
{
    public class SlotReel : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private RectTransform contentRect;

        [Header("Animation Settings")] 
        [SerializeField] private float spinSpeed = 1500f;
        [Tooltip("How high the reel jumps before spinning down")]
        [SerializeField] private float startJumpStrength = 50f;
        [SerializeField] private float startJumpDuration = 0.2f;
        [SerializeField] private float stopLandingDuration = 0.1f;
        [SerializeField] private Ease stopEaseType = Ease.OutBack;

        [Header("Grid Settings")]
        [SerializeField] private float itemHeight = 350f;
        [SerializeField] private float spacing = 100f;

        private int _symbolCount;
        private float _stride;    
        private float _loopHeight;
        
        private int _centerOffsetIndex;

        public void Initialize(int symbolCount)
        {
            _symbolCount = symbolCount;
            _stride = itemHeight + spacing;
            _loopHeight = _stride * _symbolCount;
            
            _centerOffsetIndex = _symbolCount / 2; // Integer division gets the middle index (e.g. 7 / 2 = 3)
            Shuffle();
        }

        public void Spin(float duration, int targetIndex, float delay) 
            => StartCoroutine(SpinRoutine(duration, targetIndex, delay));

        private IEnumerator SpinRoutine(float duration, int targetIndex, float delay)
        {
            var startY = contentRect.anchoredPosition.y;
            yield return contentRect.DOAnchorPosY(startY + startJumpStrength, startJumpDuration)
                .SetEase(Ease.OutQuad)
                .WaitForCompletion();

            // Spinning Loop
            float elapsed = 0f;
            float totalSpinTime = duration + delay; 
            while (elapsed < totalSpinTime)
            {
                Vector2 pos = contentRect.anchoredPosition;
                pos.y -= spinSpeed * Time.deltaTime;
                
                if (pos.y <= -_loopHeight)
                    pos.y += _loopHeight;

                contentRect.anchoredPosition = pos;
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Landing Calculation
            float baseTargetY = (targetIndex - _centerOffsetIndex) * _stride; 
            float currentY = contentRect.anchoredPosition.y;
            float destinationY = baseTargetY;
            
            while (destinationY > currentY)
                destinationY -= _loopHeight;
            
            destinationY -= _loopHeight;
            
            // Final Animation
            yield return contentRect.DOAnchorPosY(destinationY, stopLandingDuration)
                .SetEase(stopEaseType)
                .WaitForCompletion();
            
            float normalizedY = destinationY % _loopHeight;
            contentRect.anchoredPosition = new Vector2(0, normalizedY);
        }

        private void Shuffle()
        {
            int randomSymbolIndex = Random.Range(0, _symbolCount);
            
            float startY = (randomSymbolIndex - _centerOffsetIndex) * _stride;
            startY %= _loopHeight;
            
            contentRect.anchoredPosition = new Vector2(0, startY);
        }
    }
}

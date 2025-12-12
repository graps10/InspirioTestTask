using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CurrencyUI: MonoBehaviour
    {
        private const float View_Tween_Duration = 0.5f;
            
        [SerializeField] private TextMeshProUGUI diamondsText;
        [SerializeField] private TextMeshProUGUI coinsText;
        
        private void OnEnable() => CurrencyManager.OnCoinsChanged += OnCoinsChanged;

        private void OnDisable() => CurrencyManager.OnCoinsChanged -= OnCoinsChanged;
        
        private void Start()
        {
            UpdateView(CurrencyManager.Coins, false);
            diamondsText.text = CurrencyManager.Diamonds.ToString();
        }

        private void OnCoinsChanged(int newAmount) => UpdateView(newAmount, true);

        private void UpdateView(int amount, bool animate)
        {
            if (animate)
            {
                int current = int.Parse(coinsText.text);
                DOTween.To(() => current, x => coinsText.text = x.ToString(), amount, View_Tween_Duration)
                    .SetEase(Ease.OutQuad); 
            }
            else
                coinsText.text = amount.ToString();
        }
    }
}
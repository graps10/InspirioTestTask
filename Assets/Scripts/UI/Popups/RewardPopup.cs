using TMPro;
using UnityEngine;

namespace UI.Popups
{
    public class RewardPopup : PopupBase
    {
        [Header("Content")]
        [SerializeField] private TextMeshProUGUI amountText;

        public void Setup(int amount)
        {
            if (amountText)
                amountText.text = amount.ToString();
            
            base.Show();
        }

        protected override void Hide()
        {
           // AudioManager.PlaySound("UI_Close");
            base.Hide();
        }
    }
}
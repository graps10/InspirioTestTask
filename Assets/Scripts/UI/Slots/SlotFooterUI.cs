using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Slots
{
    public class SlotFooterUI : MonoBehaviour
    {
        private const float Highlight_Scale_Mult = 1.1f;
        private const float Highlight_Duration = 0.2f;
        private const int Highlight_Loops = 2;
        
        [Header("References")]
        [Tooltip("Text for center messages like 'Good Luck' or 'Try Again'")]
        [SerializeField] private TextMeshProUGUI centerText; 
        [Tooltip("Text for win messages like 'You Won' (aligned left)")]
        [SerializeField] private TextMeshProUGUI leftText;
       
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private GameObject coinIcon;

        [Header("Animation")]
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetState(string message, bool isCenterMessage, int amount = 0, bool highlight = false)
        {
            if (isCenterMessage)
                ShowCenterMessage(message);
            else
                ShowWinMessage(message, amount);

            if (highlight)
                PlayHighlightAnim();
        }
        
        private void ShowCenterMessage(string message)
        {
            if(centerText) 
            {
                centerText.gameObject.SetActive(true);
                centerText.text = message;
            }
            
            if(leftText) leftText.gameObject.SetActive(false);
            if(amountText) amountText.gameObject.SetActive(false);
            if (coinIcon) coinIcon.SetActive(false);
        }

        private void ShowWinMessage(string message, int amount)
        {
            if(centerText) centerText.gameObject.SetActive(false);
            
            if(leftText)
            {
                leftText.gameObject.SetActive(true);
                leftText.text = message;
            }

            if (amountText)
            {
                amountText.text = amount.ToString();
                amountText.gameObject.SetActive(true);
            }
            
            if (coinIcon) coinIcon.SetActive(true);
        }

        private void PlayHighlightAnim()
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
            transform.DOScale(Highlight_Scale_Mult, Highlight_Duration)
                .SetLoops(Highlight_Loops, LoopType.Yoyo);
        }
    }
}
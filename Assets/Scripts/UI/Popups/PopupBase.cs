using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PopupBase : MonoBehaviour
    {
        private const float Alpha_Visible = 1f;
        private const float Alpha_Hidden = 0f;
        private const float Scale_Visible = 1f;
        private const float Scale_Hidden = 0.7f;
        
        [Header("Animation Settings")] 
        [SerializeField] private float animDuration = 0.3f;
        [SerializeField] private Ease showEase = Ease.OutBack;
        [SerializeField] private Ease hideEase = Ease.InBack;

        [Header("Base Components")] 
        [SerializeField] protected Button closeButton;
        
        private CanvasGroup _canvasGroup;
        private RectTransform _contentRect;

        protected Action OnCloseCallback;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            if (transform.childCount > 0)
                _contentRect = transform.GetChild(0).GetComponent<RectTransform>();
            
            CloseInstant();
        }

        private void OnEnable()
        {
            if (closeButton)
                closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            if (closeButton)
                closeButton.onClick.RemoveListener(Hide);
        }

        public virtual void Show(Action onClose = null)
        {
            OnCloseCallback = onClose;
            
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = Alpha_Hidden;
            
            _canvasGroup.DOFade(Alpha_Visible, animDuration);
            
            if (_contentRect)
            {
                _contentRect.localScale = Vector3.one * Scale_Hidden;
                _contentRect.DOScale(Scale_Visible, animDuration).SetEase(showEase);
            }
        }

        protected virtual void Hide()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(Alpha_Hidden, animDuration);
            
            if (_contentRect)
            {
                _contentRect.DOScale(Scale_Hidden, animDuration)
                    .SetEase(hideEase)
                    .OnComplete(() => { OnCloseCallback?.Invoke(); });
            }
            else
                OnCloseCallback?.Invoke();
        }

        private void CloseInstant()
        {
            _canvasGroup.alpha = Alpha_Hidden;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
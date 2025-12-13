using DG.Tweening;
using UnityEngine;

namespace SceneControl
{
    public class SplashController : MonoBehaviour
    {
        private const float Alpha_Hidden = 0f;
        private const float Alpha_Visible = 1f;
        private const float Scale_Start_Mult = 0.5f;
        private const float Scale_End_Mult = 1f;
        
        [Header("UI References")] 
        [SerializeField] private CanvasGroup logoCanvasGroup;
        [SerializeField] private RectTransform logoRect;

        [Header("Settings")] [SerializeField] private float animationDuration = 1.5f;
        [SerializeField] private float delayBeforeLoad = 0.5f;
        [SerializeField] private string nextSceneName = "MainMenu";

        private void Start()
        {
            logoCanvasGroup.alpha = Alpha_Hidden;
            logoRect.localScale = Vector3.one * Scale_Start_Mult;

            Sequence seq = DOTween.Sequence();
            
            seq.Append(logoCanvasGroup.DOFade(Alpha_Visible, animationDuration));
            seq.Join(logoRect.DOScale(Scale_End_Mult, animationDuration).SetEase(Ease.OutBack));
            
            seq.AppendInterval(delayBeforeLoad);
            seq.OnComplete(() => 
            { 
                SceneLoader.Instance.LoadScene(nextSceneName); 
            });
        }
    }
}
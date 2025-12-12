using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControl
{
    public class SceneLoader: MonoBehaviour
    {
        private const float Fade_In_Alpha = 1f;
        private const float Fade_Out_Alpha = 0f;
            
        public static SceneLoader Instance;
        
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
            
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

        public async void LoadScene(string sceneName)
        {
            try
            {
                canvasGroup.blocksRaycasts = true;
                await canvasGroup.DOFade(Fade_In_Alpha, fadeDuration).AsyncWaitForCompletion();
            
                var loadOp = SceneManager.LoadSceneAsync(sceneName);
                while (loadOp != null && !loadOp.isDone)
                    await Task.Yield();
            
                await canvasGroup.DOFade(Fade_Out_Alpha, fadeDuration).AsyncWaitForCompletion();
                canvasGroup.blocksRaycasts = false;
            }
            catch (Exception e)
            {
                Debug.LogError($"SceneLoading Error: {e.Message}");
            }
        }
    }
}
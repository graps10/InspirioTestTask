using Awards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Awards
{
    public class AwardUI : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image unlockedImage;
        [SerializeField] private Image lockImage;

        public void Setup(AwardData data, bool isUnlocked)
        {
            titleText.text = data.Title;
            descriptionText.text = data.Description;

            if (isUnlocked)
            {
                unlockedImage.sprite = data.UnlockedIcon;
                unlockedImage.gameObject.SetActive(true);
                lockImage.gameObject.SetActive(false);
            }
            else
            {
                lockImage.sprite = data.LockedIcon;
                lockImage.gameObject.SetActive(true);
                unlockedImage.gameObject.SetActive(false);
            }
        }
    }
}
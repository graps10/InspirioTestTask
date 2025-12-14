using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Leaderboard
{
    public class ChallengeUI : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descText;
        [SerializeField] private TextMeshProUGUI multiplierText;
        [SerializeField] private Image background;

        public void Setup(global::Board.ChallengeData data)
        {
            titleText.text = data.Title;
            descText.text = data.Description;
            multiplierText.text = $"X{data.RewardMultiplier}";

            if (background) background.sprite = data.BackgroundSprite;
        }
    }
}
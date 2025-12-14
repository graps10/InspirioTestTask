using Board;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Leaderboard
{
    public class FriendUI : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void Setup(UserData data)
        {
            if (avatarImage) avatarImage.sprite = data.Avatar;
            if (scoreText) scoreText.text = $"{data.Score}";
        }
    }
}
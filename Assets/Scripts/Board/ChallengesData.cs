using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "NewChallenge", menuName = "Game/Challenge Data")]
    public class ChallengeData : ScriptableObject
    {
        public string Title;      
        public string Description;
        public int RewardMultiplier = 2;
        public Sprite BackgroundSprite;
    }
}
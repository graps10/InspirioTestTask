using UnityEngine;

namespace Awards
{
    public enum AwardRewardType
    {
        Trophy,
        CoinToken,
        Crown
    }
    
    public enum AwardConditionType
    {
        TotalSpins,
        TotalCoins,
        WorkoutsFinished
    }

    [CreateAssetMenu(fileName = "NewAward", menuName = "Game/Award Data")]
    public class AwardData : ScriptableObject
    {
        [Header("Content")]
        public string Title;
        public string Description;
        
        [Header("Visuals")]
        public Sprite UnlockedIcon;
        public Sprite LockedIcon;
        
        [Header("Condition Logic")]
        public AwardConditionType ConditionType;
        public int TargetValue;

        [Header("Reward")]
        public AwardRewardType RewardType;
        public int RewardAmount;
    }
}
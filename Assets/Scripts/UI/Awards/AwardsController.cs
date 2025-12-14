using System;
using System.Collections.Generic;
using Awards;
using TMPro;
using UnityEngine;

namespace UI.Awards
{
    public class AwardsController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private List<AwardData> awardsData;

        [Header("UI Slots (Static)")]
        [SerializeField] private List<AwardUI> awardSlots;

        [Header("Bottom Panel Counters")]
        [SerializeField] private TextMeshProUGUI trophyCountText;
        [SerializeField] private TextMeshProUGUI coinTokenCountText;
        [SerializeField] private TextMeshProUGUI crownCountText;

        private void Start() => RefreshUI();

        private void RefreshUI()
        {
            int trophies = 0;
            int tokens = 0;
            int crowns = 0;
            
            for (int i = 0; i < awardsData.Count; i++)
            {
                if (i >= awardSlots.Count) break;

                AwardData data = awardsData[i];
                bool isUnlocked = CheckIfUnlocked(data);
                
                awardSlots[i].Setup(data, isUnlocked);
                
                if (isUnlocked)
                {
                    switch (data.RewardType)
                    {
                        case AwardRewardType.Trophy: trophies += data.RewardAmount; break;
                        case AwardRewardType.CoinToken: tokens += data.RewardAmount; break;
                        case AwardRewardType.Crown: crowns += data.RewardAmount; break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            
            trophyCountText.text = $"X{trophies}";
            coinTokenCountText.text = $"X{tokens}";
            crownCountText.text = $"X{crowns}";
        }

        private bool CheckIfUnlocked(AwardData data)
        {
            int currentProgress = data.ConditionType switch
            {
                AwardConditionType.TotalCoins => CurrencyManager.Coins
                ,
                AwardConditionType.TotalSpins => GameDataManager.TotalSpins,
                AwardConditionType.WorkoutsFinished => GameDataManager.WorkoutsCompleted,
                _ => 0
            };

            return currentProgress >= data.TargetValue;
        }
    }
}
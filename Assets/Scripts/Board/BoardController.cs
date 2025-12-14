using System.Collections.Generic;
using UI.Leaderboard;
using UnityEngine;

namespace Board
{
    public class BoardController : MonoBehaviour
    {
        [Header("Data Source")]
        [SerializeField] private List<UserData> friendsData;
        [SerializeField] private List<ChallengeData> challengesData;

        [Header("UI References")]
        [SerializeField] private List<FriendUI> friendSlots;
        [SerializeField] private List<ChallengeUI> challengeSlots;

        private void Start()
        {
            RefreshLeaderboard();
            RefreshChallenges();
        }

        private void RefreshLeaderboard()
        {
            friendsData.Sort((a, b) => b.Score.CompareTo(a.Score));

            for (int i = 0; i < friendSlots.Count; i++)
            {
                if (i < friendsData.Count)
                {
                    friendSlots[i].gameObject.SetActive(true);
                    friendSlots[i].Setup(friendsData[i]);
                }
                else
                    friendSlots[i].gameObject.SetActive(false);
            }
        }

        private void RefreshChallenges()
        {
            for (int i = 0; i < challengeSlots.Count; i++)
            {
                if (i < challengesData.Count)
                {
                    challengeSlots[i].gameObject.SetActive(true);
                    challengeSlots[i].Setup(challengesData[i]);
                }
                else
                    challengeSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
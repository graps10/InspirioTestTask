using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "NewFriend", menuName = "Game/Friend Data")]
    public class UserData : ScriptableObject
    {
        public string UserName;
        public Sprite Avatar;
        public int Score;
    }
}
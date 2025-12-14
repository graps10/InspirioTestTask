using UnityEngine;

public static class GameDataManager
{
    private const string Key_Coins = "Save_Coins";
    private const string Key_TotalSpins = "Stats_TotalSpins";
    private const string Key_Workouts = "Stats_WorkoutsDone";
    
    private const string Key_Sound = "Settings_Sound";
    private const string Key_Music = "Settings_Music";
    private const string Key_Vibration = "Settings_Vibration";
    
    public static int Coins
    {
        get => PlayerPrefs.GetInt(Key_Coins, 0);
        set
        {
            PlayerPrefs.SetInt(Key_Coins, value);
            Save();
        }
    }
    
    public static int TotalSpins
    {
        get => PlayerPrefs.GetInt(Key_TotalSpins, 0);
        private set
        {
            PlayerPrefs.SetInt(Key_TotalSpins, value);
            Save();
        }
    }

    public static int WorkoutsCompleted
    {
        get => PlayerPrefs.GetInt(Key_Workouts, 0);
        private set
        {
            PlayerPrefs.SetInt(Key_Workouts, value);
            Save();
        }
    }
    
    public static bool IsSoundOn
    {
        get => PlayerPrefs.GetInt(Key_Sound, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(Key_Sound, value ? 1 : 0);
            Save();
        }
    }

    public static bool IsMusicOn
    {
        get => PlayerPrefs.GetInt(Key_Music, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(Key_Music, value ? 1 : 0);
            Save();
        }
    }

    public static bool IsVibrationOn
    {
        get => PlayerPrefs.GetInt(Key_Vibration, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(Key_Vibration, value ? 1 : 0);
            Save();
        }
    }
    
    public static void IncrementTotalSpins() => TotalSpins++;

    public static void IncrementWorkouts() => WorkoutsCompleted++;

    private static void Save() => PlayerPrefs.Save();
}
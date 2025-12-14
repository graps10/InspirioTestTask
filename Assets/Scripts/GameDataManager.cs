using UnityEngine;

public static class GameDataManager
{
    private const string Key_Coins = "Save_Coins";
    private const string Key_TotalSpins = "Stats_TotalSpins";
    private const string Key_Workouts = "Stats_WorkoutsDone";
    
    private const string Key_SoundVol = "Settings_Sound_Vol";
    private const string Key_MusicVol = "Settings_Music_Vol";
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
    
    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat(Key_MusicVol, 1f);
        set
        {
            PlayerPrefs.SetFloat(Key_MusicVol, value);
            PlayerPrefs.Save();
        }
    }

    public static float SoundVolume
    {
        get => PlayerPrefs.GetFloat(Key_SoundVol, 1f);
        set
        {
            PlayerPrefs.SetFloat(Key_SoundVol, value);
            PlayerPrefs.Save();
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
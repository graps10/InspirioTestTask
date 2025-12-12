using System;
using UnityEngine;

public static class CurrencyManager
{
    private const string Coins_Key = "Coins_Save";
    private const int Default_Coins = 100;
    
    public static event Action<int> OnCoinsChanged;

    public static int Coins
    {
        get => PlayerPrefs.GetInt(Coins_Key, Default_Coins);
        private set
        {
            PlayerPrefs.SetInt(Coins_Key, value);
            PlayerPrefs.Save();
            OnCoinsChanged?.Invoke(value);
        }
    }
    
    public static int Diamonds => 2300;

    public static void AddCoins(int amount)
    {
        Coins += amount;
    }

    public static bool TrySpendCoins(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            return true;
        }

        return false;
    }
}

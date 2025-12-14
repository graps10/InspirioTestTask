using System;

public static class CurrencyManager
{
    public static event Action<int> OnCoinsChanged;

    public static int Coins
    {
        get => GameDataManager.Coins;
        private set 
        {
            GameDataManager.Coins = value;
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

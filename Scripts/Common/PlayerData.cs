using System;
using UnityEngine;

public class PlayerData : MonoBehaviour, IInitializable, IWalletOperatible
{
    public static PlayerData Instance;

    public Action CoinsCountChanged;

    public int Coins { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Initialize()
    {
        Coins = PlayerPrefs.GetInt("Coins", 0);
    }
    public void DecreaseCoins(object sender, int count)
    {
        if (sender is IWalletOperatible)
            Coins -= count;

        Save();
    }
    public void IncreaseCoins(object sender, int count)
    {
        if (sender is IWalletOperatible)
            Coins += count;

        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetInt("Coins", Coins);
        CoinsCountChanged?.Invoke();
    }
}
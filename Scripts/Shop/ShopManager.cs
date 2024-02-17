using System;
using UnityEngine;

public class ShopManager : MonoBehaviour, IWalletOperatible
{
    public static ShopManager Instance;

    public Action SkinChanged;

    [SerializeField] private ShopItem[] _items;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        PlayerPrefs.SetInt("ShopItem0", 1);

        int id = 0;
        foreach (ShopItem item in _items)
        {
            item.Initialize(id, this);
            item.UpdateItem(PlayerPrefs.GetInt("ShopItemSelected", 0));
            id++;
        }
    }
    public void UpdateItems()
    {
        foreach (ShopItem item in _items)
        {
            item.UpdateItem(PlayerPrefs.GetInt("ShopItemSelected", 0));
        }

        SkinChanged?.Invoke();
    }
    public bool TryToBuyItem(ShopItem item, int price)
    {
        if (PlayerData.Instance.Coins >= price)
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.PopUp, 1, 0.6f);

            PlayerPrefs.SetInt("ShopItem" + item.Id(), 1);
            PlayerPrefs.SetInt("ShopItemSelected", item.Id());

            PlayerData.Instance.DecreaseCoins(this, price);
            return true;
        }
        else
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Win, 1, 0.6f);
            return false;
        }
    }
}

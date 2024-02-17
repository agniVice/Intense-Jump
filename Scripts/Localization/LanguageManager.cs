using System;
using System.Collections.Generic;
using UnityEngine;

public enum Languange
{
    English,
    Russian
}

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public Action LanguageChanged;

    public Languange CurrentLanguage;

    public List<LocalizedString> LocalizedStrings = new List<LocalizedString>();

    [SerializeField] private List<string> _english;
    [SerializeField] private List<string> _russian;

    /* KEYS
     * 0 PLAY
     * 1 ENGLISH
     * 2 BACK
     * 3 SHOP
     * 4 SOON
     * 5 EXIT
     * 6 BEST
     * 7 JUMP
     * 8 SCORE
     * 9 GAME OVER
     * 10 RESTART
     * 11 MENU
     * 12 COINS
    */

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Initialize();
    }
    private void Initialize()
    {
        CurrentLanguage = (Languange)PlayerPrefs.GetInt("Language", 0);
    }
    public void ToggleLanguange()
    {
        if (CurrentLanguage == Languange.English)
            CurrentLanguage = Languange.Russian;
        else if(CurrentLanguage == Languange.Russian)
            CurrentLanguage = Languange.English;

        PlayerPrefs.SetInt("Language", (int)CurrentLanguage);

        foreach (LocalizedString localizedString in LocalizedStrings)
        {
            if (localizedString != null)
            { 
                if(localizedString.AutoLocalize())
                    localizedString.LocalizeMe();
            }
        }

        LanguageChanged?.Invoke();
    }
    public string GetTranslate(int keyIndex)
    {
        if (CurrentLanguage == Languange.English)
        {
            if (_english.Count > keyIndex)
                return _english[keyIndex];
            else
                return "Unknown";
        }
        else if(CurrentLanguage == Languange.Russian)
        {
            if (_russian.Count > keyIndex)
                return _russian[keyIndex];
            else
                return "Unknown";
        }
        return "Unknown";
    }
    public void ChangeLanguage(int id)
    {
        CurrentLanguage = (Languange)id;

        PlayerPrefs.SetInt("Language", (int)CurrentLanguage);

        foreach (LocalizedString localizedString in LocalizedStrings)
        {
            if (localizedString != null)
                localizedString.LocalizeMe();
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedString : MonoBehaviour
{
    [SerializeField] private int _keyId;
    [SerializeField] private bool _autoLocalize = true;

    private TextMeshProUGUI _text;

    private void Start()
    {
        LanguageManager.Instance.LocalizedStrings.Add(this);
        _text = GetComponent<TextMeshProUGUI>();
        if(_autoLocalize)
            LocalizeMe();
    }
    public void LocalizeMe()
    {
        GetLocalized(_keyId);
    }
    private void GetLocalized(int keyId)
    {
        if (LanguageManager.Instance != null)
            _text.text = LanguageManager.Instance.GetTranslate(keyId);
        else
            _text.text = "ERROR";
    }
    public int KeyId()
    { 
        return _keyId;
    }
    public bool AutoLocalize() 
    {
        return _autoLocalize; 
    }
}

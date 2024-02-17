using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Sprite _ballSprite;
    [SerializeField] private int _price;

    private Image _selectedIcon;
    private TextMeshProUGUI _priceText;

    private ShopManager _shopManager;

    private bool _available;
    private int _id;

    public void Initialize(int id, ShopManager shopManager)
    {
        _id = id;
        _shopManager = shopManager;

        GetComponent<Button>().onClick.AddListener(SelectThis);
        GetComponent<Image>().sprite = _ballSprite;

        _selectedIcon = transform.GetChild(0).GetComponent<Image>();
        _priceText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (PlayerPrefs.GetInt("ShopItem" + id) == 1)
            _available = true;
        else
            _available = false;
    }
    public void UpdateItem(int selectedItem)
    {
        if (selectedItem == _id)
        {
            _selectedIcon.gameObject.SetActive(true);
            _priceText.gameObject.SetActive(false);
        }
        else
        {
            if (_available)
                _priceText.gameObject.SetActive(false);
            else
                _priceText.gameObject.SetActive(true);

            _selectedIcon.gameObject.SetActive(false);
        }
    }
    public void SelectThis()
    {
        if (_available)
        {
            PlayerPrefs.SetInt("ShopItemSelected", _id);
        }
        else
        {
            if (_shopManager.TryToBuyItem(this, _price))
                _available = true;
        }

        _shopManager.UpdateItems();
    }
    public int Id()
    { 
        return _id;
    }
}

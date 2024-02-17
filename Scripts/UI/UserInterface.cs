using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour, IInitializable
{
    public static UserInterface Instance;

    [SerializeField] private List<GameObject> _panelPrefabs = new List<GameObject>();

    private MenuHUD _menuHUD;
    private ShopHUD _shopHUD;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public void Initialize()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;

        foreach (GameObject panel in _panelPrefabs)
        {
            if (panel.TryGetComponent(out ISubscriber subscriber))
                subscriber.SubscribeAll();

            if (panel.TryGetComponent(out IInitializable initializable))
                initializable.Initialize();
        }

        _menuHUD = FindObjectOfType<MenuHUD>();
        _shopHUD = FindObjectOfType<ShopHUD>();

    }
    public void OpenMenu()
    {
        if (_menuHUD != null)
            _menuHUD.Show();
        if (_shopHUD != null)
            _shopHUD.Hide();
    }
    public void OpenShop()
    {
        if (_menuHUD != null)
            _menuHUD.Hide();
        if (_shopHUD != null)
            _shopHUD.Show();
    }
}
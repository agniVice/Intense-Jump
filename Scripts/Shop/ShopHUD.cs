using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ShopHUD : MonoBehaviour, IHUDElement, ISubscriber, IInitializable
{
    [SerializeField] private TextMeshProUGUI _coins;

    [SerializeField] private List<IStartAnim> _startAnims;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
    }
    public void Initialize()
    {
        _startAnims = GetComponentsInChildren<IStartAnim>().ToList();

        Hide();
    }
    public void SubscribeAll()
    {
        AppState.Instance.GameStarted += Hide;
        PlayerData.Instance.CoinsCountChanged += UpdateCoinsText;
    }
    public void UnsubscribeAll()
    {
        AppState.Instance.GameStarted -= Hide;
        PlayerData.Instance.CoinsCountChanged -= UpdateCoinsText;
    }
    public void Hide()
    {
        _canvasGroup.gameObject.SetActive(false);
    }
    public void Show()
    {
        UpdateCoinsText();

        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 0.3f).SetLink(_canvasGroup.gameObject);

        foreach (IStartAnim anim in _startAnims)
            anim.PlayStartAnim();
    }
    public void OnBackButtonClicked()
    {
        UserInterface.Instance.OpenMenu();
    }
    private void UpdateCoinsText()
    {
        _coins.transform.DOScale(1.2f, 0.15f).SetLink(_coins.gameObject);
        _coins.transform.DOScale(1f, 0.15f).SetLink(_coins.gameObject).SetDelay(0.15f).SetEase(Ease.OutBack);

        _coins.text = LanguageManager.Instance.GetTranslate(_coins.GetComponent<LocalizedString>().KeyId()) + " " + PlayerData.Instance.Coins;
    }
}

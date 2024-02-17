using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MenuHUD : MonoBehaviour, IHUDElement, ISubscriber, IInitializable
{
    [SerializeField] private TextMeshProUGUI _bestScore;

    [SerializeField] private List<IStartAnim> _startAnims;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
    }
    public void Initialize()
    {
        _startAnims = GetComponentsInChildren<IStartAnim>().ToList();
        UpdateScoreText();
    }
    public void SubscribeAll()
    {
        AppState.Instance.GameStarted += Hide;
        LanguageManager.Instance.LanguageChanged += UpdateScoreText;
    }
    public void UnsubscribeAll()
    {
        AppState.Instance.GameStarted -= Hide;
        LanguageManager.Instance.LanguageChanged -= UpdateScoreText;
    }
    public void Hide()
    {
        _canvasGroup.gameObject.SetActive(false);
    }
    public void Show()
    {
        UpdateScoreText();

        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 0.3f).SetLink(_canvasGroup.gameObject);

        foreach (IStartAnim anim in _startAnims)
            anim.PlayStartAnim();
    }
    public void OnPlayButtonClicked()
    {
        AppState.Instance.StartGame();
    }
    public void OnLanguageButtonClicked()
    {
        LanguageManager.Instance.ToggleLanguange();
    }
    public void OnShopButtonClicked()
    {
        UserInterface.Instance.OpenShop();
    }
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
    private void UpdateScoreText()
    {
        _bestScore.text = LanguageManager.Instance.GetTranslate(_bestScore.GetComponent<LocalizedString>().KeyId()) + " " + PlayerPrefs.GetInt("HighScore", 0);
    }
}

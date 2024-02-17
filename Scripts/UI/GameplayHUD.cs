using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameplayHUD : MonoBehaviour, IHUDElement, ISubscriber, IInitializable
{
    [SerializeField] private TextMeshProUGUI _score;

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
        AppState.Instance.GameRestarted += Show;
        AppState.Instance.GameStarted += Show;
        AppState.Instance.GameFinished += Hide;
        AppState.Instance.ScoreAdded += UpdateScoreText;
    }
    public void UnsubscribeAll()
    {
        AppState.Instance.GameRestarted += Show;
        AppState.Instance.GameStarted -= Show;
        AppState.Instance.GameFinished -= Hide;
        AppState.Instance.ScoreAdded -= UpdateScoreText;
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
    public void OnJumpButtonClicked()
    {
        PlayerInput.Instance.TryToMouseDown();
    }
    private void UpdateScoreText()
    {
        _score.text = LanguageManager.Instance.GetTranslate(_score.GetComponent<LocalizedString>().KeyId()) + " " + PlayerScore.Instance.Score;
    }
}

using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameOverHUD : MonoBehaviour, IHUDElement, ISubscriber, IInitializable
{
    [SerializeField] private TextMeshProUGUI _bestScore;
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
        AppState.Instance.GameRestarted += Hide;
        AppState.Instance.GameFinished += Show;
        LanguageManager.Instance.LanguageChanged += UpdateScoreText;
    }
    public void UnsubscribeAll()
    {
        AppState.Instance.GameRestarted += Hide;
        AppState.Instance.GameFinished -= Show;
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
    public void OnRestartButtonClicked()
    {
        AppState.Instance.RestartGame();
    }
    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1.0f;
        SceneLoader.Instance.LoadScene("Gameplay");
    }
    private void UpdateScoreText()
    {
        _bestScore.text = LanguageManager.Instance.GetTranslate(_bestScore.GetComponent<LocalizedString>().KeyId()) + " " + PlayerPrefs.GetInt("HighScore", 0);
        _score.text = LanguageManager.Instance.GetTranslate(_score.GetComponent<LocalizedString>().KeyId()) + " " + PlayerScore.Instance.Score;
    }
}

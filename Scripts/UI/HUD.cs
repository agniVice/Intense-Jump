using UnityEngine;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

public class HUD : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private CanvasGroup _tip;

    [SerializeField] private List<Transform> _transforms = new List<Transform>();

    private bool _isInitialized;

    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        SubscribeAll();
    }
    private void OnDisable()
    {
        UnsubscribeAll();
    }
    private void UpdateScore()
    {
        _scoreText.text = PlayerScore.Instance.Score.ToString();
        _scoreText.transform.localScale = Vector2.zero;
        _scoreText.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetLink(_scoreText.gameObject);
    }
    public void Initialize()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;

        _tip.alpha = 0f;
        _tip.DOFade(1, 0.3f).SetLink(_tip.gameObject);

        UpdateScore();

        Show();

        _isInitialized = true;
    }
    public void SubscribeAll()
    {
        AppState.Instance.GameStarted += HideTip;
        AppState.Instance.GameFinished += Hide;
        AppState.Instance.GamePaused += Hide;
        AppState.Instance.GameUnpaused += Show;

        AppState.Instance.ScoreAdded += UpdateScore;
    }
    public void UnsubscribeAll()
    {
        AppState.Instance.GameStarted -= HideTip;
        AppState.Instance.GameFinished -= Hide;
        AppState.Instance.GamePaused -= Hide;
        AppState.Instance.GameUnpaused -= Show;

        AppState.Instance.ScoreAdded -= UpdateScore;
    }
    private void HideTip()
    { 
        _tip.DOFade(0, 0.3f).SetLink(_tip.gameObject);
    }
    private void Show()
    {
        _panel.SetActive(true);

        foreach (Transform transform in _transforms)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, Random.Range(0.2f, 0.7f)).SetEase(Ease.OutBack).SetLink(transform.gameObject).SetUpdate(true);
        }
    }
    private void Hide()
    {
        _panel.SetActive(false);
    }
    public void OnRestartButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }
    public void OnButtonPauseClicked()
    {
        AppState.Instance.PauseGame();
    }
    public void OnExitButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Menu");
    }
}
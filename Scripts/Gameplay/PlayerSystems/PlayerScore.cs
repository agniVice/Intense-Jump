using DG.Tweening;
using UnityEngine;

public class PlayerScore : MonoBehaviour, ISubscriber, IInitializable, IWalletOperatible
{
    public static PlayerScore Instance;

    public int Score { get; private set; }

    private bool _isInitialized;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
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
    public void SubscribeAll()
    {
        AppState.Instance.GameRestarted += ResetScore;
        AppState.Instance.GameFinished += Save;
    }

    public void UnsubscribeAll()
    {
        AppState.Instance.GameRestarted -= ResetScore;
        AppState.Instance.GameFinished -= Save;
    }

    public void Initialize()
    {
        _isInitialized = true;
    }
    public void ResetScore()
    {
        Score = 0;
    }
    public void AddScore()
    {
        Score++;
        AppState.Instance.ScoreAdded?.Invoke();
        Camera.main.DOShakePosition(0.2f, 0.1f, fadeOut: true).SetUpdate(true);
        Camera.main.DOShakeRotation(0.2f, 0.1f, fadeOut: true).SetUpdate(true);
    }
    private void Save()
    {
        if (Score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", Score);

        PlayerData.Instance.IncreaseCoins(this, Score);
    }
}
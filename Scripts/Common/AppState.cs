using DG.Tweening;
using System;
using UnityEngine;

public class AppState : MonoBehaviour, IInitializable
{
    public static AppState Instance;

    public Action GameStarted;
    public Action GamePaused;
    public Action GameUnpaused;
    public Action GameFinished;
    public Action GameRestarted;

    public Action ScoreAdded;
    public State CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public void Initialize()
    {
        CurrentState = State.Ready;
    }
    public void StartGame()
    {
        GameStarted?.Invoke();
        CurrentState = State.InGame;
        Time.timeScale = 1.0f;
    }
    public void PauseGame()
    {
        GamePaused?.Invoke();
        CurrentState = State.Paused;
        Time.timeScale = 0.0f;
    }
    public void UnpauseGame()
    {
        GameUnpaused?.Invoke();
        CurrentState = State.InGame;
        Time.timeScale = 1.0f;
    }
    public void FinishGame()
    {
        GameFinished?.Invoke();
        CurrentState = State.Finished;

        PlayerInput.Instance.IsEnabled = false;

        Time.timeScale = 1.0f;

        Camera.main.DOShakePosition(0.5f, 0.3f, fadeOut: true).SetUpdate(true);
        Camera.main.DOShakeRotation(0.5f, 0.3f, fadeOut: true).SetUpdate(true);

        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Win, 1f);

        /*if (AudioVibrationManager.Instance.IsVibrationEnabled)
            Handheld.Vibrate();*/
    }
    public void RestartGame()
    {
        GameRestarted?.Invoke();
        CurrentState = State.Ready;

        PlayerInput.Instance.IsEnabled = true;

        Time.timeScale = 1.0f;
    }
}
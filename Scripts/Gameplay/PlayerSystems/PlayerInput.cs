using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour, IInitializable, ISubscriber
{ 
    public static PlayerInput Instance;

    public Action PlayerMouseDown;
    public Action PlayerMouseUp;

    public bool IsEnabled = true;// { get; private set; }

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
    public void Initialize()
    {
        SubscribeAll();
    }
    public void SubscribeAll()
    {
        AppState.Instance.GameRestarted += EnableInput;
        AppState.Instance.GameStarted += EnableInput;
    }
    public void UnsubscribeAll()
    {
        AppState.Instance.GameRestarted -= EnableInput;
        AppState.Instance.GameStarted -= EnableInput;
    }
    public void EnableInput()
    { 
        IsEnabled= true;
    }
    public void DisableInput() 
    {
        IsEnabled = false;
    }
    private void OnMouseDown()
    {
        TryToMouseDown();
    }
    private void OnMouseUp()
    {
        TryToMouseUp();
    }
    public void TryToMouseDown()
    {
        if (!IsEnabled)
            return;

        PlayerMouseDown?.Invoke();
    }
    public void TryToMouseUp()
    {
        if (!IsEnabled)
            return;

        PlayerMouseUp?.Invoke();
    }
}
using DG.Tweening;
using UnityEngine;

public class ScaleUpAnim : MonoBehaviour, IStartAnim
{
    [SerializeField] private Ease _animEase = Ease.OutBack;
    [SerializeField] private float _time = 0.15f;

    [Range(0, 1)]
    [SerializeField] private float _randomDifference = 0;
    [SerializeField] private bool _autoPlay = false;

    private Vector2 _startScale;

    private void Awake()
    {
        _startScale = transform.localScale;

        if (_autoPlay)
            PlayStartAnim();
    }
    public void PlayStartAnim()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(_startScale, GetAnimTime()).SetLink(gameObject).SetEase(_animEase);
    }
    private float GetAnimTime()
    {
        float difference = Random.Range(-0.15f, 0.15f);
        return _time + difference * _randomDifference;

    }
}

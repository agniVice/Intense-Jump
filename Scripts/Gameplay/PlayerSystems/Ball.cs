using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Ball : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] _particlePrefab;
    [SerializeField] private Sprite[] _ballSprites;
    [SerializeField] private Color32[] _ballColors;

    [Space]
    [Header("BallSettings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 5f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private ColorType _colorType;

    private Vector2 _direction = Vector2.right;
    public Vector2 Direction => _direction;

    private bool _isMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();

        _rigidbody.simulated = false;

        SetRandomColor();
        SetSprite();
    }
    private void OnEnable()
    {
        ShopManager.Instance.SkinChanged += SetSprite;
        PlayerInput.Instance.PlayerMouseDown += OnPlayerMouseDown;
    }
    private void OnDisable()
    {
        ShopManager.Instance.SkinChanged -= SetSprite;
        PlayerInput.Instance.PlayerMouseDown -= OnPlayerMouseDown;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void SetSprite()
    {
        _spriteRenderer.sprite = _ballSprites[PlayerPrefs.GetInt("ShopItemSelected", 0)];
    }
    private void SetRandomColor()
    {
        _colorType = (ColorType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ColorType)).Length);

        _spriteRenderer.color = _ballColors[(int)_colorType];
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab[(int)_colorType]).GetComponent<ParticleSystem>();

        if (_direction.x > 0)
            particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else
            particle.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);

        particle.transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    private void OnPlayerMouseDown()
    {
        if (_isMoving == false)
        {
            _isMoving = true;
            _rigidbody.simulated = true;

            Jump();
        }
        else
            Jump();
    }
    private void Move()
    {
        if (_isMoving)
        {
            _rigidbody.velocity = new Vector2(_direction.normalized.x * _speed, _rigidbody.velocity.y);
        }
    }
    private void ChangeDirection()
    {
        _rigidbody.AddTorque(UnityEngine.Random.Range(1, 5)* _direction.x);
        _direction *= new Vector2(-1, 0);
    }
    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpHeight);
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.PopUp, UnityEngine.Random.Range(0.85f, 1.1f), 0.8f);
    }
    private void EnableCollider()
    {
        _collider.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Section"))
        {
            if (collision.gameObject.GetComponent<Section>().ColorType == _colorType)
            {
                ChangeDirection();

                _collider.enabled = false;
                Invoke("EnableCollider", 0.2f);
                transform.position = new Vector2(transform.position.x + _direction.x * 0.05f, transform.position.y);

                SpawnParticle();
                SetRandomColor();

                PlayerScore.Instance.AddScore();
                AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ScoreAdd, UnityEngine.Random.Range(0.85f, 1.1f));
            }
            else
            {
                AppState.Instance.FinishGame();
                _collider.enabled = false;
                SpawnParticle();
            }
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            AppState.Instance.FinishGame();
            _collider.enabled = false;
            SpawnParticle();
        }
    }
    public ColorType ColorType => _colorType;
}

using UnityEngine;

public class BallSpawner : MonoBehaviour, ILevelSpawner
{
    [SerializeField] private Transform _spawnPosition;

    [SerializeField] private GameObject _ballPrefab;

    private Ball _ball;
    private void OnEnable()
    {
        AppState.Instance.GameRestarted += Respawn;
    }
    private void OnDisable()
    {
        AppState.Instance.GameRestarted -= Respawn;
    }
    private void Respawn()
    {
        Clear();
        Build();
    }
    public void Build()
    {
        _ball = Instantiate(_ballPrefab, transform.position, Quaternion.identity).GetComponent<Ball>();
    }
    public void Clear()
    {
        Destroy(_ball.gameObject);
        _ball = null;
    }
}

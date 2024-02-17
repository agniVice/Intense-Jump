using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour, ILevelSpawner
{
    [SerializeField] private Transform _leftPosition;
    [SerializeField] private Transform _rightPosition;

    [SerializeField] private GameObject _obstaclePrefab;

    private List<Obstacle> _obstacles = new List<Obstacle>();


    private void OnEnable()
    {
        AppState.Instance.GameRestarted += Respawn;
        AppState.Instance.ScoreAdded += Respawn;
    }
    private void OnDisable()
    {
        AppState.Instance.GameRestarted -= Respawn;
        AppState.Instance.ScoreAdded -= Respawn;
    }
    private void Respawn()
    {
        Clear();
        Build();
    }
    public void Build()
    {
        var obstacle = Instantiate(_obstaclePrefab, _leftPosition.position, Quaternion.identity, transform).GetComponent<Obstacle>();
        _obstacles.Add(obstacle);

        obstacle.Initialize();

        obstacle = Instantiate(_obstaclePrefab, _rightPosition.position, Quaternion.identity, transform).GetComponent<Obstacle>();
        _obstacles.Add(obstacle);

        obstacle.IsRightSide = true;

        obstacle.Initialize();
    }

    public void Clear()
    {
        foreach (var obstacle in _obstacles)
            Destroy(obstacle.gameObject);

        _obstacles.Clear();
    }
}

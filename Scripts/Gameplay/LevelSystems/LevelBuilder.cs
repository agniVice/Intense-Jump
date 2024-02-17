using UnityEngine;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour, IInitializable
{
    public static LevelBuilder Instance;

    public GameObject[] SpawnerPrefabs;
    private List<ILevelSpawner> _spawners = new List<ILevelSpawner>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    public void Initialize()
    {
        BuildLevel();
    }
    public void BuildLevel()
    {
        foreach (GameObject spawner in SpawnerPrefabs)
        {
            var currentSpawner = Instantiate(spawner);

            _spawners.Add(currentSpawner.GetComponent<ILevelSpawner>());

            currentSpawner.GetComponent<ILevelSpawner>().Build();
        }
    }
}
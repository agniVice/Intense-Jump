using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabsFirstLevel;
    [SerializeField] private List<GameObject> _prefabsSecondLevelWithGreen;
    [SerializeField] private List<GameObject> _prefabsSecondLevelWithOrange;
    [SerializeField] private List<GameObject> _prefabsThirdLevelWithGreen;
    [SerializeField] private List<GameObject> _prefabsThirdLevelWithOrange;

    public bool IsRightSide;

    private GameObject _obstacle;
    public void Initialize()
    {
        _obstacle = Instantiate(GetRandomPrefab(), transform);
    }
    public void Clear()
    {
        Destroy(_obstacle.gameObject);
    }
    private GameObject GetRandomPrefab()
    {
        var score = PlayerScore.Instance.Score;
        var ball = FindObjectOfType<Ball>();

        if (ball.Direction.x > 0)
        {
            if (IsRightSide)
            {
                if (score >= 5)
                {
                    if (ball.ColorType == 0)
                        return _prefabsThirdLevelWithOrange[Random.Range(0, _prefabsThirdLevelWithOrange.Count)];
                    else
                        return _prefabsThirdLevelWithGreen[Random.Range(0, _prefabsThirdLevelWithGreen.Count)];
                }
                if (score >= 3)
                {
                    if (ball.ColorType == 0)
                        return _prefabsSecondLevelWithOrange[Random.Range(0, _prefabsSecondLevelWithOrange.Count)];
                    else
                        return _prefabsSecondLevelWithGreen[Random.Range(0, _prefabsSecondLevelWithGreen.Count)];
                }
                return _prefabsFirstLevel[(int)ball.ColorType];
            }
            else
            {
                if (score >= 5)
                {
                    if ((int)ball.ColorType == 1)
                        return _prefabsThirdLevelWithOrange[Random.Range(0, _prefabsThirdLevelWithOrange.Count)];
                    else
                        return _prefabsThirdLevelWithGreen[Random.Range(0, _prefabsThirdLevelWithGreen.Count)];
                }
                if (score >= 3)
                {
                    if ((int)ball.ColorType == 1)
                        return _prefabsThirdLevelWithOrange[Random.Range(0, _prefabsThirdLevelWithOrange.Count)];
                    else
                        return _prefabsThirdLevelWithGreen[Random.Range(0, _prefabsThirdLevelWithGreen.Count)];
                }

                if (ball.ColorType == 0)
                    return _prefabsFirstLevel[1];
                else
                    return _prefabsFirstLevel[0];
            }
        }
        else
        {
            if (!IsRightSide)
            {
                if (score >= 5)
                {
                    if (ball.ColorType == 0)
                        return _prefabsThirdLevelWithOrange[Random.Range(0, _prefabsThirdLevelWithOrange.Count)];
                    else
                        return _prefabsThirdLevelWithGreen[Random.Range(0, _prefabsThirdLevelWithGreen.Count)];
                }
                if (score >= 3)
                {
                    if (ball.ColorType == 0)
                        return _prefabsSecondLevelWithOrange[Random.Range(0, _prefabsSecondLevelWithOrange.Count)];
                    else
                        return _prefabsSecondLevelWithGreen[Random.Range(0, _prefabsSecondLevelWithGreen.Count)];
                }
                return _prefabsFirstLevel[(int)ball.ColorType];
            }
            else
            {
                if (score >= 5)
                {
                    if ((int)ball.ColorType == 1)
                        return _prefabsThirdLevelWithOrange[Random.Range(0, _prefabsThirdLevelWithOrange.Count)];
                    else
                        return _prefabsThirdLevelWithGreen[Random.Range(0, _prefabsThirdLevelWithGreen.Count)];
                }
                if (score >= 3)
                {
                    if ((int)ball.ColorType == 1)
                        return _prefabsThirdLevelWithOrange[Random.Range(0, _prefabsThirdLevelWithOrange.Count)];
                    else
                        return _prefabsThirdLevelWithGreen[Random.Range(0, _prefabsThirdLevelWithGreen.Count)];
                }

                if (ball.ColorType == 0)
                    return _prefabsFirstLevel[1];
                else
                    return _prefabsFirstLevel[0];
            }
        }
    }
}

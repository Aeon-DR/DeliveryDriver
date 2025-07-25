using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnScoreIncreased;

    private int _score;

    private void OnEnable()
    {
        PackageDelivery.OnPackageDelivered += IncreaseScore;
    }

    private void OnDisable()
    {
        PackageDelivery.OnPackageDelivered -= IncreaseScore;
    }

    private void IncreaseScore()
    {
        _score++;
        OnScoreIncreased?.Invoke(_score);
        Debug.Log($"Score: {_score}.");
    }
}

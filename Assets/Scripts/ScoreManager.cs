using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnScoreIncreased;
    public static int Score;
    public static int BestScore;

    private void OnEnable()
    {
        PackageDelivery.OnPackageDelivered += IncreaseScore;
    }

    private void OnDisable()
    {
        PackageDelivery.OnPackageDelivered -= IncreaseScore;
    }

    private void Start()
    {
        Score = 0; // Reset score at the start of the game
    }

    private void IncreaseScore()
    {
        Score++;
        OnScoreIncreased?.Invoke(Score);
        Debug.Log($"Score: {Score}.");
    }
}

using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameOver;

    private int _timer = 60;

    private void Start()
    {
        StartCoroutine(CountdownTimerRoutine());
    }

    private void GamerOver()
    {
        OnGameOver?.Invoke();
        StopAllCoroutines();

        Debug.Log($"Game Over! Final Score: {ScoreManager.Score}");
        if (ScoreManager.Score > ScoreManager.BestScore)
        {
            ScoreManager.BestScore = ScoreManager.Score;
            Debug.Log($"New Best Score: {ScoreManager.BestScore}");
        }
        else
        {
            Debug.Log($"Best Score remains: {ScoreManager.BestScore}");
        }
    }

    private IEnumerator CountdownTimerRoutine()
    {
        while (_timer > 0)
        {
            Debug.Log($"Time left: {_timer} seconds.");
            yield return new WaitForSeconds(1);
            _timer--;
        }

        GamerOver();
    }
}

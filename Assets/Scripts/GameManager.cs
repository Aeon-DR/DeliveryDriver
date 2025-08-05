using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameOver;
    public static event Action<int> OnTimerChanged;
    public static int Timer = 60;

    private void Start()
    {
        StartCoroutine(CountdownTimerRoutine());
    }

    private void GamerOver()
    {
        if (ScoreManager.Score > ScoreManager.BestScore)
        {
            ScoreManager.BestScore = ScoreManager.Score;
        }

        OnGameOver?.Invoke();
        StopAllCoroutines();
    }

    private IEnumerator CountdownTimerRoutine()
    {
        while (Timer > 0)
        {
            yield return new WaitForSeconds(1);
            Timer--;
            OnTimerChanged?.Invoke(Timer);
        }

        GamerOver();
    }
}

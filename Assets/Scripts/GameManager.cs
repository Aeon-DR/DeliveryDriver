using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameOver;
    public static event Action<int> OnTimerChanged;
    public static int Timer = 60;
    public static bool IsGameOver;

    private void Start()
    {
        IsGameOver = false;
        StartCoroutine(CountdownTimerRoutine());
    }

    private void GamerOver()
    {
        if (ScoreManager.Score > ScoreManager.BestScore)
        {
            ScoreManager.BestScore = ScoreManager.Score;
        }

        IsGameOver = true;
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

    public void RestartScene()
    {
        Timer = 60;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverContainer;
    [SerializeField] private GameObject _penaltyMessage;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    
    private void OnEnable()
    {
        ScoreManager.OnScoreIncreased += UpdateScoreUI;
        GameManager.OnTimerChanged += UpdateTimerUI;
        GameManager.OnGameOver += ShowGameResults;
        CarController.OnMovementFreeze += ShowPenaltyMessage;
        CarController.OnMovementUnfreeze += HidePenaltyMessage;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreIncreased -= UpdateScoreUI;
        GameManager.OnTimerChanged -= UpdateTimerUI;
        GameManager.OnGameOver -= ShowGameResults;
        CarController.OnMovementFreeze -= ShowPenaltyMessage;
        CarController.OnMovementUnfreeze -= HidePenaltyMessage;
    }

    private void Start()
    {
        _scoreText.text = $"Score: {ScoreManager.Score}";
        _timerText.text = $"Time Left: {GameManager.Timer}";
        _gameOverContainer.SetActive(false);
    }

    private void UpdateScoreUI(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

    private void UpdateTimerUI(int timer)
    {
        _timerText.text = $"Time Left: {timer}";
    }

    private void ShowGameResults()
    {
        _gameOverContainer.SetActive(true);
        _finalScoreText.text = $"Final Score: {ScoreManager.Score}";
        _bestScoreText.text = $"Best Score: {ScoreManager.BestScore}";
        HidePenaltyMessage(); // Ensure penalty message is hidden on game over
    }

    private void ShowPenaltyMessage()
    {
        _penaltyMessage.SetActive(true);
    }

    private void HidePenaltyMessage()
    {
        _penaltyMessage.SetActive(false);
    }
}

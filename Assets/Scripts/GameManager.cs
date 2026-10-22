using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Rules")]
    [SerializeField] private int pointsToWin = 3;
    [SerializeField] private int maxAttempts = 3;

    [Header("UI")]
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text attemptsText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Player Control")]
    [Tooltip("Drag in your player controller / input scripts. They get disabled when the game ends.")]
    [SerializeField] private Behaviour[] controlsToDisable;

    public bool IsGameOver { get; private set; }

    private int points;
    private int attemptsRemaining;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        points = 0;
        attemptsRemaining = maxAttempts;
        IsGameOver = false;

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        UpdateUI();
    }

    // Called by DeliveryZone
    public void AddPoint()
    {
        if (IsGameOver) return;

        points++;
        UpdateUI();

        if (points >= pointsToWin)
            EndGame(true);
    }

    // Called by HazardZone
    public void LoseAttempt()
    {
        if (IsGameOver) return;

        attemptsRemaining--;
        UpdateUI();

        if (attemptsRemaining <= 0)
            EndGame(false);
    }

    private void EndGame(bool won)
    {
        IsGameOver = true;

        // Take away player control
        foreach (Behaviour b in controlsToDisable)
        {
            if (b != null) b.enabled = false;
        }

        if (won && winPanel != null) winPanel.SetActive(true);
        if (!won && losePanel != null) losePanel.SetActive(true);

        Debug.Log(won ? "You Win!" : "You Lose!");
    }

    private void UpdateUI()
    {
        if (pointsText != null)
            pointsText.text = $"Points: {points} / {pointsToWin}";

        if (attemptsText != null)
            attemptsText.text = $"Attempts: {attemptsRemaining}";
    }

    // Hook this up to a Restart button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

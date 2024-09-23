using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameTime = 30f; // Total game time in seconds
    private float timer;
    public Text timerText; // UI Text to display the remaining time
    public Text finalScoreText; // Displays the final score on the game over screen

    private bool isGameOver = false;

    void Start()
    {
        timer = gameTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Countdown the timer
            timer -= Time.deltaTime;
            UpdateTimerUI();

            if (timer <= 0)
            {
                GameOver(); // Trigger Game Over when the timer reaches 0
            }
        }
    }

    // Update the timer UI text
    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
        }
    }

    // Game Over function
    public void GameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
        ScreenManager.instance.ChangeScreen(ScreenManager.ScreenType.gameOver);
        // Display the final score
        finalScoreText.text = "Final Score: " + ScoreManager.Instance.GetScore().ToString();
    }

    // Function to restart the game
    public void RestartGame()
    {
        // Reload the current scene to restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}




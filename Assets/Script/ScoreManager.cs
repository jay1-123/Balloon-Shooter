using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;
    private int combo = 1;
    public float comboTimeWindow = 2f; // Time window for maintaining a combo
    private float comboTimer;

    public Text scoreText;   // Reference to the score text UI
    public Text comboText;   // Reference to the combo text UI

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        UpdateScoreUI();
        UpdateComboUI();
    }

    // Call this method when a balloon is popped to add points
    public void AddScore(int points)
    {
        score += points * combo;
        combo++;  // Increase combo for consecutive hits
        comboTimer = comboTimeWindow;  // Reset the combo timer

        UpdateScoreUI();
        UpdateComboUI();
    }

    // Reset combo multiplier (if player misses or timer runs out)
    public void ResetCombo()
    {
        combo = 1;  // Reset combo multiplier to 1
        UpdateComboUI();
    }

    void Update()
    {
        // Countdown combo timer if player has an active combo
        if (combo > 1)
        {
            comboTimer -= Time.deltaTime;
            Debug.Log(comboTimer);
            if (comboTimer <= 0f)
            {
                ResetCombo();  // Reset combo if timer runs out
            }
        }
    }

    // Update the score text on the UI
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Update the combo multiplier text on the UI
    void UpdateComboUI()
    {
        if (comboText != null)
        {
            if (combo > 1)
            {
                comboText.text = "Combo x" + combo.ToString();
            }
            else
            {
                comboText.text = ""; // Clear the combo text when no combo is active
            }
        }
    }
}

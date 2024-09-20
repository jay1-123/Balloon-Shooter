using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    public float speed = 2f;           // General speed for balloon movement
    public float waveAmplitude = 2f;   // Amplitude for wavy movement
    public float waveFrequency = 2f;   // Frequency for wavy movement
    public float zigzagAmplitude = 2f; // Amplitude for zig-zag movement
    public float zigzagFrequency = 2f; // Frequency for zig-zag movement

    public enum MovementPattern { Straight, Wavy, ZigZag }
    private MovementPattern currentPattern;
    private Vector3 startPosition;

    void Start()
    {
        InitializeMovement();
    }

    // Call this method when the balloon is spawned to reset its movement pattern
    public void InitializeMovement()
    {
        startPosition = transform.position;
        currentPattern = (MovementPattern)Random.Range(0, 3); // Randomly select a movement pattern (Straight, Wavy, ZigZag)
    }

    void Update()
    {
        // Move balloon according to the selected movement pattern
        switch (currentPattern)
        {
            case MovementPattern.Straight:
                MoveStraight();
                break;
            case MovementPattern.Wavy:
                MoveWavy();
                break;
            case MovementPattern.ZigZag:
                MoveZigZag();
                break;
        }

        // If the balloon goes off-screen, deactivate it
        if (transform.position.y > Camera.main.orthographicSize + 5f) // Add 5 units as buffer
        {
            gameObject.SetActive(false);
        }
    }

    // Straight movement pattern: Moves straight upward
    void MoveStraight()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // Wavy movement pattern: Moves upward while oscillating horizontally in a sinusoidal pattern
    void MoveWavy()
    {
        // Vertical movement
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Horizontal sinusoidal movement
        float horizontalOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        transform.position = new Vector3(startPosition.x + horizontalOffset, transform.position.y, transform.position.z);
    }

    // Zig-zag movement pattern: Moves upward with sharp alternating horizontal shifts
    void MoveZigZag()
    {
        // Vertical movement
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Sharp alternating horizontal movement (zig-zag pattern)
        float horizontalOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        transform.position = new Vector3(startPosition.x + horizontalOffset, transform.position.y, transform.position.z);
    }
}

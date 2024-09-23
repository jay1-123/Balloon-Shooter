
using UnityEngine;
using UnityEngine.Scripting;

public class StartScreen : MonoBehaviour
{

    private void Awake()
    {
        Time.timeScale = 0;
    }
    public void OnStartButtonClick()
    {
        ScreenManager.instance.ChangeScreen(ScreenManager.ScreenType.gameScreen);
        Time.timeScale = 1;  // Start the game
    }
}

using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public BaseScreen[] screens;
    public BaseScreen currentScreen;
    public static ScreenManager instance;

    public void Awake()
    {
        Time.timeScale = 0;
        instance = this;
        currentScreen.canvas.enabled = true;
    }
    private void Start()
    {
        
        Time.timeScale = 0;
    }
    public void ChangeScreen(ScreenType screentype)
    {
        currentScreen.canvas.enabled = false;

        foreach (BaseScreen screen in screens)
        {
            if (screen.screenType == screentype)
            {
                screen.canvas.enabled = true;
                currentScreen = screen;
                break;
            }
        }
    }

    public enum ScreenType
    {
        start,
        gameScreen,
        gameOver
        
    }
    [System.Serializable]
    public class BaseScreen
    {
        public ScreenType screenType;
        public Canvas canvas;
    }
}


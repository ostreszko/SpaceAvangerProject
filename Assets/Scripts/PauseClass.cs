using UnityEngine;
using UnityEngine.SceneManagement;

//Klasa odpowadająca za pauzowanie, resetowanie rozgrywki oraz wyjście do menu
public class PauseClass : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public static PauseClass Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (StaticStates.ActualState == (int)StaticStates.States.Pause || StaticStates.ActualState == (int)StaticStates.States.GameOver)
        {
            EscapeMainMenu();
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if ((StaticStates.ActualState == (int)StaticStates.States.Pause) && (StaticStates.ActualState == (int)StaticStates.States.Pause || StaticStates.ActualState != (int)StaticStates.States.GameOver))
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        StaticStates.ActualState = (int)StaticStates.States.Game;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        StaticStates.ActualState = (int)StaticStates.States.Pause;
    }

    public void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
            Time.timeScale = 1f;
            StaticStates.ActualState = (int)StaticStates.States.Pause;
        }
    }

    public void EscapeMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            StaticStates.ActualState = (int)StaticStates.States.Menu;
            SceneManager.LoadScene("Menu");
        }
    }
}

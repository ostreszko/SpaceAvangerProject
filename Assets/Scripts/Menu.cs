using UnityEngine;
using UnityEngine.SceneManagement;

//Klasa odpowiadająca za przełączanie się międy menu gry, oraz odpalenie sceny z grą
public class Menu : MonoBehaviour
{
    public Menu()
    {
        StaticStates.ActualState = (int)StaticStates.States.Menu;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetDifficulty(float difficulty)
    {
            StaticDifficulty.GameDifficulty = difficulty;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

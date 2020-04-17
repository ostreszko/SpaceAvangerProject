//Stany gry
public class StaticStates 
{
    public enum States
    {
        Menu = 1,
        Game = 2,
        Pause = 3,
        GameOver = 4
    }

    public static int ActualState { get; set; }
}

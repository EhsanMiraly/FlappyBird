

public class GameData
{
    public static bool IsPlaying { get; set; } = false;
    public const float GameSpeed = 15f;


    private static int points { get; set; } = 0;
    public static int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
            EventsManager.InvokePointsChanged_Event();
        }
    }
}

public delegate void OnNotify();

public class EventsManager
{
    public static event OnNotify NewGame_Event;
    public static void InvokeNewGame_Event()
    {
        NewGame_Event?.Invoke();
    }


    public static event OnNotify LoseGame_Event;
    public static void InvokeLoseGame_Event()
    {
        LoseGame_Event?.Invoke();
    }


    public static event OnNotify PointsChanged_Event;
    public static void InvokePointsChanged_Event()
    {
        PointsChanged_Event?.Invoke();
    }


}

using UnityEngine;

public static class State
{
    public static Speed_States Current_State { get; private set; }

    private static readonly RangeAttribute easy_speed_range = new RangeAttribute(0, 35);
    private static readonly RangeAttribute normal_speed_range = new RangeAttribute(easy_speed_range.max, 150);
    private static readonly RangeAttribute high_speed_range = new RangeAttribute(normal_speed_range.max, 200);
    private static readonly RangeAttribute infinity_speed_range = new RangeAttribute(high_speed_range.max, int.MaxValue);

    public static void Setting_state(int game_score, ref double duration_increment)
    {
        if (game_score >= easy_speed_range.min && game_score <= easy_speed_range.max)
        {
            if (Current_State == Speed_States.Start)
            {
                Easy_speed(ref duration_increment);
            }
        }
        else if (game_score > normal_speed_range.min && game_score <= normal_speed_range.max)
        {
            if (Current_State == Speed_States.Easy_speed)
            {
                Normal_speed(ref duration_increment);
            }
        }
        else if (game_score > high_speed_range.min && game_score <= high_speed_range.max)
        {
            if (Current_State == Speed_States.Normal_speed)
            {
                High_speed(ref duration_increment);
            }
        }
        else if (game_score > infinity_speed_range.min && game_score <= infinity_speed_range.max)
        {
            if (Current_State == Speed_States.High_speed)
            {
                Infinity_speed(ref duration_increment);
            }
        }
    }
    public static void Setting_state()
    {
        Restart_State();
    }

    private static void Easy_speed(ref double _duration_increment)
    {
        Current_State = Speed_States.Easy_speed;
        _duration_increment /= easy_speed_range.max;
    }
    private static void Normal_speed(ref double _duration_increment)
    {
        Current_State = Speed_States.Normal_speed;
        _duration_increment /= normal_speed_range.max;
    }
    private static void High_speed(ref double _duration_increment)
    {
        Current_State = Speed_States.High_speed;
        _duration_increment /= high_speed_range.max;
    }
    private static void Infinity_speed(ref double _duration_increment)
    {
        Current_State = Speed_States.Infinity_increment;
        _duration_increment /= infinity_speed_range.min;
    }
    private static void Restart_State()
    {
        Current_State = Speed_States.Start;
    }
}

public enum Speed_States
{
    Start,
    Easy_speed,
    Normal_speed,
    High_speed,
    Infinity_increment
}

using UnityEngine;

public class TimeManager
{
    public Countdown MainTimer => _mainTimer;
    private Countdown _mainTimer = new Countdown(new TimeLimit(0));
}

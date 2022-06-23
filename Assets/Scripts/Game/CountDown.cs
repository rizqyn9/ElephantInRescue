using System.Collections;
using UnityEngine;


[System.Serializable]
public class CountDown
{
    public int CountTotal { get; private set; }
    public int CurrentTime { get; private set; }
    public Coroutine Coroutine { get; private set; }

    HeaderUtils HeaderUtils { get; set; }

    public CountDown(HeaderUtils headerUtils)
    {
        HeaderUtils = headerUtils;
        CountTotal = HeaderUtils.LevelManager.LevelConfiguration.CountDown;
    }

    public void Start()
    {
        Coroutine = HeaderUtils.StartCoroutine(ICountDown());
    }

    public void Stop()
    {
        HeaderUtils.StopCoroutine(Coroutine);
    }

    IEnumerator ICountDown()
    {
        CurrentTime = CountTotal;
        while (CurrentTime > 0)
        {
            yield return new WaitForSeconds(1);
            CurrentTime--;
            HeaderUtils.OnCountDownChange();
        }
        OnTimeOut();
    }

    void OnTimeOut()
    {
        HeaderUtils.OnTimeOut();
    }

    public int GetRemainingTime() => CurrentTime;
}

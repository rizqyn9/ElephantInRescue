using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderUtils : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text countText;

    public CountDown CountDown { get; set; }
    public LevelManager LevelManager { get; set; }

    private void OnEnable()
    {
        LevelManager = LevelManager.Instance;
        CountDown = new CountDown(this);
    }

    internal void OnCountDownChange()
    {
        print("asdasx");
        countText.text = CountDown.CurrentTime.ToString();
    }

    internal void OnTimeOut()
    {
        print("Time out");
    }
}

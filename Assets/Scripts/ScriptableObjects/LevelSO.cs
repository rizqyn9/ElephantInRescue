using UnityEngine;

[CreateAssetMenu(menuName = "EIR/Create Level")]
public class LevelSO : DescriptionBaseSO
{
    public int Level;
    public int Stage;
    public int CountDown;
    public Color32 ElephantColor;
    public GameObject GO_Tutorial;
    public GameObject GO_MainComponent;
    public Sprite LevelTitle;
    public int Star3, Star2, Star1;

    public bool ShouldTutorialUI { get => GO_Tutorial != null; }

    public bool IsComingSoon { get => !GO_MainComponent; }

    /// <summary>
    /// Get the level whic exist on resource manager
    /// </summary>
    /// <param name="resources"></param>
    /// <param name="stage"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static LevelSO FindLevel(ResourcesManager resources, int stage, int level) =>
        resources.ListLevels.Find(levelExist => levelExist.Level == level && levelExist.Stage == stage);

    public WinLoseType CalculateStar(int remainingTime)
    {
        int timeLength = CountDown - remainingTime;
        Debug.Log(timeLength);
        if (remainingTime == 0) return WinLoseType.TIME_OUT;
        else if (timeLength <= Star3) return WinLoseType.STARS3;
        else if (timeLength <= Star2) return WinLoseType.STARS2;
        else if (timeLength < Star1) return WinLoseType.STARS1;
        else return WinLoseType.TIME_OUT;
    }
}

///
/// param = 44
/// rt = 60 - 20
/// rt = 40
/// 
///
///

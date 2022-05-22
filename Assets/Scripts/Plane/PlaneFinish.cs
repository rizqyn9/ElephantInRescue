using UnityEngine;

public class PlaneFinish : Plane
{
    [SerializeField] VoidEventChannelSO m_handleOnFinish;

    public override void Start()
    {
        base.Start();
        PlaneType = PlaneTypeEnum.FINISH;
    }

    /// <summary>
    /// Do Something TODO
    /// - Win the game
    /// </summary>
    public override void OnElephant()
    {
        print("Win");
        m_handleOnFinish?.RaiseEvent();
    }
}

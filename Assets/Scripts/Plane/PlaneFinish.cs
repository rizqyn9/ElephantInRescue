using UnityEngine;

public class PlaneFinish : Plane
{
    [SerializeField] VoidEventChannelSO m_handleOnFinish;

    private void Start()
    {
        PlaneType = PlaneTypeEnum.FINISH;        
    }

    /// <summary>
    /// Do Something TODO
    /// - Win the game
    /// </summary>
    public override void OnElephant()
    {
        m_handleOnFinish?.RaiseEvent();
    }
}

using UnityEngine;

public class CivilianBarier : Civilian
{
    internal override void OnPlay()
    {
        base.OnPlay();

        CivilianMovement.Start();
        CivilianFOV.Start();
    }
}

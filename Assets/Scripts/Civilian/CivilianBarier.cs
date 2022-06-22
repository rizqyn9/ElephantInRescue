using UnityEngine;

public class CivilianBarier : Civilian
{
    internal override void OnPlay()
    {
        base.OnPlay();

        StartCoroutine(CivilianMovement.IStartMove());
    }
}

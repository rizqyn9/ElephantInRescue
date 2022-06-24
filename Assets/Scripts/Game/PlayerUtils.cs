using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerUtils
{
    public static bool ShouldMove(Plane plane) =>
        plane.PlaneType == PlaneTypeEnum.ROUTE
                        || plane.PlaneType == PlaneTypeEnum.FINISH
                        || (plane.PlaneType == PlaneTypeEnum.TREE && (plane as PlaneTree).Destroyed);
}


public static class PlayerUtils
{
    public static bool ShouldMove(Plane plane) =>
        (!plane.Box && !plane.Civilian) &&
        plane.PlaneType == PlaneTypeEnum.ROUTE
        || plane.PlaneType == PlaneTypeEnum.FINISH
        || (plane.PlaneType == PlaneTypeEnum.TREE && (plane as PlaneTree).Destroyed);
}

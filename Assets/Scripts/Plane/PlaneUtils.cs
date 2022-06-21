using System.Collections.Generic;
using UnityEngine;

public static class PlaneUtils
{
    /// <summary>
    /// Generate 4 direction top left right bottom
    /// </summary>
    private static List<Vector2> AxisDirections = new List<Vector2>() { Vector2.down, Vector2.up, Vector2.left, Vector2.right };

    /// <summary>
    /// Get All axis if direction empty will filled by AxisDirectios
    /// </summary>
    /// <param name="currentPlane"></param>
    /// <param name="directions"></param>
    public static List<Plane> GetAllAxis(Plane currentPlane, List<Vector2> directions = null)
    {
        List<Plane> planes = new List<Plane>();
        directions ??= AxisDirections;
        foreach (Vector2 dir in directions)
        {
            Plane plane = GetDirection(currentPlane, dir);
            if (plane) planes.Add(plane);
        }

        return planes;
    }

    /// <summary>
    /// Get Plane from current Plane Specific
    /// </summary>
    /// <param name="currentPlane"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    public static Plane GetDirection(Plane currentPlane, Vector2 direction, float distance = 1f)
    {
        Plane plane = null;
        RaycastHit2D[] hits = Physics2D
            .RaycastAll(currentPlane.transform.position, direction, distance);

        foreach (RaycastHit2D hit in hits)
        {
            plane = hit.collider.GetComponent<Plane>();
            if (plane && (plane.name != currentPlane.name)) break;
        }

        return plane;
    }
}

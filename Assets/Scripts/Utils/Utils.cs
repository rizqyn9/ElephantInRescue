using UnityEngine;

public static class Utils
{
    public static Vector2 DecideDirection (Vector2 pos1, Vector2 pos2)
    {
        Vector3 direction = Vector3.zero;

        if (Mathf.Abs(pos2.x - pos1.x) > Mathf.Abs(pos2.y - pos1.y))
        {
            if (pos2.x > pos1.x)
            {
                direction = Vector3.right;
            }
            else if (pos2.x < pos1.x)
            {
                direction = Vector3.left;
            }
        }

        else if (Mathf.Abs(pos2.x - pos1.x) < Mathf.Abs(pos2.y - pos1.y))
        {
            if (pos2.y > pos1.y)
            {
                direction = Vector3.up;
            }
            else if (pos2.y < pos1.y)
            {
                direction = Vector3.down;
            }
        }

        return direction;
    }
}
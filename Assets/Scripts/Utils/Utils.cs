using UnityEngine;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// Returns true if the scene 'name' exists and is in your Build settings, false otherwise
    /// </summary>
    public static bool DoesSceneExist(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var lastSlash = scenePath.LastIndexOf("/");
            var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

            if (string.Compare(name, sceneName, true) == 0)
                return true;
        }

        return false;
    }

    public static RaycastHit2D RaycastCamera(Vector3 position, Vector2 dir, LayerMask layerMask = default(LayerMask)) =>
        Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), dir, layerMask);

    public static LevelSO FindLevelResource(ResourcesManager resources, int stage, int level)
    {
        return resources.ListLevels.Find(levelExist => (levelExist.Stage == stage) && (levelExist.Level == level));
    }
}
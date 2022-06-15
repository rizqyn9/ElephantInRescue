using UnityEditor;
using UnityEngine;

public class BuildScript
{
    [MenuItem("EIR/Remove JSON")]
    public static void RemovePersistantJSON()
    {
        Debug.Log("Remove");
    }

    // BuildScript.PerformBuild
    [MenuItem("EIR/Enable development build")]
    public static void PerformBuild()
    {
        EditorUserBuildSettings.development = true;
    }
}

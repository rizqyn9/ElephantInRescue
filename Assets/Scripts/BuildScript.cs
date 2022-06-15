using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildScript
{
    [MenuItem("EIR/Remove JSON")]
    public static void RemovePersistantJSON()
    {
        try
        {
            File.Delete(Application.persistentDataPath + "/eir_production.json");
        } catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    // BuildScript.PerformBuild
    [MenuItem("EIR/Enable development build")]
    public static void PerformBuild()
    {
        Debug.Log("Set as development build");
        EditorUserBuildSettings.development = true;
    }
}

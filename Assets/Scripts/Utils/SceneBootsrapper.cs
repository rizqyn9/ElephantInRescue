#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;
//#endif

//#if UNITY_EDITOR
[InitializeOnLoad]
public class SceneBootstrapper
{
    const string k_BootstrapSceneKey = "BootstrapScene";
    const string k_PreviousSceneKey = "PreviousScene";
    const string k_LoadBootstrapSceneKey = "LoadBootstrapScene";

    const string k_LoadBootstrapSceneOnPlay = "EIR/Load Bootstrap Scene On Play";
    const string k_DoNotLoadBootstrapSceneOnPlay = "EIR/Don't Load Bootstrap Scene On Play";

    static bool s_StoppingAndStarting;

    static string BootstrapScene
    {
        get
        {
            if (!EditorPrefs.HasKey(k_BootstrapSceneKey))
            {
                EditorPrefs.SetString(k_BootstrapSceneKey, EditorBuildSettings.scenes[0].path);
            }
            return EditorPrefs.GetString(k_BootstrapSceneKey, EditorBuildSettings.scenes[0].path);
        }
        set => EditorPrefs.SetString(k_BootstrapSceneKey, value);
    }

    static string PreviousScene
    {
        get => EditorPrefs.GetString(k_PreviousSceneKey);
        set => EditorPrefs.SetString(k_PreviousSceneKey, value);
    }

    static bool LoadBootstrapScene
    {
        get
        {
            if (!EditorPrefs.HasKey(k_LoadBootstrapSceneKey))
            {
                EditorPrefs.SetBool(k_LoadBootstrapSceneKey, true);
            }
            return EditorPrefs.GetBool(k_LoadBootstrapSceneKey, true);
        }
        set => EditorPrefs.SetBool(k_LoadBootstrapSceneKey, value);
    }

    static SceneBootstrapper()
    {
        EditorApplication.playModeStateChanged += EditorApplicationOnplayModeStateChanged;
    }

    [MenuItem(k_LoadBootstrapSceneOnPlay, true)]
    static bool ShowLoadBootstrapSceneOnPlay()
    {
        return !LoadBootstrapScene;
    }

    [MenuItem(k_LoadBootstrapSceneOnPlay)]
    static void EnableLoadBootstrapSceneOnPlay()
    {
        LoadBootstrapScene = true;
    }

    [MenuItem(k_DoNotLoadBootstrapSceneOnPlay, true)]
    static bool ShowDoNotLoadBootstrapSceneOnPlay()
    {
        return LoadBootstrapScene;
    }

    [MenuItem(k_DoNotLoadBootstrapSceneOnPlay)]
    static void DisableDoNotLoadBootstrapSceneOnPlay()
    {
        LoadBootstrapScene = false;
    }

    static void EditorApplicationOnplayModeStateChanged(PlayModeStateChange obj)
    {
        if (!LoadBootstrapScene)
        {
            return;
        }

        if (s_StoppingAndStarting)
        {
            return;
        }

        if (obj == PlayModeStateChange.ExitingEditMode)
        {
            // cache previous scene so we return to this scene after play session, if possible
            PreviousScene = EditorSceneManager.GetActiveScene().path;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // user either hit "Save" or "Don't Save"; open bootstrap scene

                if (!string.IsNullOrEmpty(BootstrapScene) &&
                    System.Array.Exists(EditorBuildSettings.scenes, scene => scene.path == BootstrapScene))
                {
                    var activeScene = EditorSceneManager.GetActiveScene();

                    s_StoppingAndStarting = activeScene.path == string.Empty ||
                        !BootstrapScene.Contains(activeScene.path);

                    // we only manually inject Bootstrap scene if we are in a blank empty scene,
                    // or if the active scene is not already BootstrapScene
                    if (s_StoppingAndStarting)
                    {
                        s_StoppingAndStarting = true;
                        EditorApplication.ExitPlaymode();

                        // scene is included in build settings; open it
                        EditorSceneManager.OpenScene(BootstrapScene);

                        EditorApplication.EnterPlaymode();
                        s_StoppingAndStarting = false;

                    }
                }
            }
            else
            {
                // user either hit "Cancel" or exited window; don't open bootstrap scene & return to editor

                EditorApplication.isPlaying = false;
            }
        }
        else if (obj == PlayModeStateChange.EnteredEditMode)
        {
            if (!string.IsNullOrEmpty(PreviousScene))
            {
                EditorSceneManager.OpenScene(PreviousScene);
            }
        }
    }
}
#endif
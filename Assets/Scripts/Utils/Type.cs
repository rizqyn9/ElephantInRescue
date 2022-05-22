using System;

[Serializable]
public struct PlayerDataModel
{
    public string userName;
    public int currentLevel;
    public int currentStage;
    public object userPreferances;
}

public enum SceneState
{
    GAME,
    MAINMENU
}

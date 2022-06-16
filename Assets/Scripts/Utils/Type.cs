using System;
using System.Collections.Generic;

[Serializable]
public struct PlayerDataModel
{
    public List<LevelDataModel> LevelDatas;
}

[Serializable]
public struct LevelDataModel
{
    public int Stage;
    public int Level;
    public bool IsNewLevel;
    public bool IsOpen;
    public int Stars;
    public int HighScore;
}

public enum SceneState
{
    GAME,
    MAINMENU
}

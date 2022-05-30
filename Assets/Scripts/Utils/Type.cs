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
    public int Level;
    public int Stage;
    public bool IsOpen;
    public int Stars;
    public bool IsNewLevel;
    public int HighScore;
}

public enum SceneState
{
    GAME,
    MAINMENU
}

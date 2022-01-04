using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Level Base", menuName = "ScriptableObject/LevelBase")]
public class LevelBase : ScriptableObject
{
    public int level;
}

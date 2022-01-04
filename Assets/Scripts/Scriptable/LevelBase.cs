using System.Collections;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Level Base", menuName = "ScriptableObject/LevelBase")]
public class LevelBase : ScriptableObject
{
    public int level;
    public List<int> stages = new List<int>();
}

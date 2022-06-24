using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] List<LevelSO> m_listLevels = new List<LevelSO>();
    [SerializeField] List<LevelDataModel> m_levelBases;

    // Accessor
    public List<LevelDataModel> LevelDatas { get => m_levelBases; }
    public List<LevelSO> ListLevels { get => m_listLevels; }
}

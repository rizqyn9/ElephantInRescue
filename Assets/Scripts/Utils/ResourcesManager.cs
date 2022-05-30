using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager _instance;
    public static ResourcesManager Instance { get => _instance; }

    [Header("Properties")]
    [SerializeField] List<LevelDataModel> m_levelBases;

    // Accessor
    public static List<LevelDataModel> LevelDatas { get => Instance.m_levelBases; } 

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


    private void Start()
    {
        print("Resources Manager Loaded");
    }
}

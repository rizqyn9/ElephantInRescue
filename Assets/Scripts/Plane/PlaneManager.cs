using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    private static PlaneManager _instance; 
    public static PlaneManager Instance { get => _instance; }


    private Plane[][] planes;

    [SerializeField] GameObject nullPlane, treePlane, wallPlane, routePlane;

    public void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;

    }


}

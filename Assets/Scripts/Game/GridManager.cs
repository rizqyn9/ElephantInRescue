using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR.Game
{
    public class GridManager : MonoBehaviour
    {
        public int rowsTotal, colsTotal, gap;
        public GameObject baseGrid;
        public Transform place;

        [ContextMenu("test")]
        public void test()
        {
            ClearGrid();
            GenerateGrid();
        }

        [ContextMenu("Clear Grid")]
        public void ClearGrid()
        {
            foreach(Transform target in place.GetComponentsInChildren<Transform>())
            {
                if (!(target.name == "--Grid"))
                {
                    DestroyImmediate(target.gameObject);
                }

            }
        }

        public void GenerateGrid()
        {
            // Generate cols
            for(int i = 0; i < colsTotal; i++)
            {
                // Generate row
                for(int j = 0; j < rowsTotal; j++)
                {
                    GameObject res = Instantiate(baseGrid, new Vector2(colsTotal / 2 - i, colsTotal / 2 - j), Quaternion.identity, place);
                    res.name = $"{i} {j}";
                }
            }
        }
    }
}

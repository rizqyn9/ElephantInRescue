using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EIR.Test {
    public class GridManager : MonoBehaviour
    {
        public List<List<int>> grids = new List<List<int>>();

        public void Start() {
            Generate();
        }

        public int Row, Col;
        public List<GameObject> rowsGO, colsGO = new List<GameObject>();

        public void Generate() {
            for(int i = 0; i < Row; i++) {

                Transform rowRender = Instantiate(new GameObject(), transform).GetComponent<Transform>();
                rowRender.position = new Vector2(rowRender.position.x, rowRender.position.y + 1);
                rowRender.name = $"Row {i}";
                rowsGO.Add(rowRender.gameObject);

                for(int j = 0; j < Col; j++) {

                    GameObject resGO = Instantiate(new GameObject(), rowRender);
                    resGO.transform.position = new Vector2(resGO.transform.position.x, resGO.transform.position.y + j);
                    resGO.name = $"Coloumn {j}";
                    colsGO.Add(resGO);

                }
            }
        }

    
    }
}

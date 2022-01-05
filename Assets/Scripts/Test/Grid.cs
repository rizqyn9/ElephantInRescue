using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EIR.Test {
    public class Grid : MonoBehaviour {
        public static Grid Instance;

        [Header("Properties")]
        public int rows;
        public int cols;
        public float gap;


        private void Awake() {
            Instance = this;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Grid))]
    public class GridEditor : Editor {

        Grid target1;
        private void OnEnable() {
            target1 = target as Grid;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if(GUILayout.Button("Grid")) {
                drawGrid();
            }

        }

        public void drawGrid() {
            GUILayout.TextArea("asdd");
            GUILayout.Button("haha");
        }

    }

    public enum Test {
        A,
        B
    }

    public class GridWindow : EditorWindow {

        public Grid source;
        public Test type;

        [MenuItem("EIR/Grid")]
        static void Init() {
            var window = GetWindowWithRect<GridWindow>(new Rect(0, 0, 500, 500));
            window.Show();
        }

        void OnGUI() {
            EditorGUILayout.BeginHorizontal();
            source = EditorGUILayout.ObjectField(source, typeof(Object), true) as Grid;
            EditorGUILayout.EndHorizontal();

                    type = (Test)EditorGUILayout.EnumPopup("Type", type);
            if (GUILayout.Button("Grid"))
            {
                for(int i = 0; i < source.cols; i++) {
                }
            }
        }
    }
#endif
}

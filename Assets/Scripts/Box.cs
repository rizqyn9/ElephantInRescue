using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Box : MonoBehaviour
{
    [SerializeField] GameStateChannelSO m_GameStateChannelSO = null;
    [SerializeField] LayerMask m_ObstacleLayerMask;
    [SerializeField] PlaneBase initalPos;
    List<Vector2> m_dirs = new List<Vector2>() { Vector2.down, Vector2.up, Vector2.left, Vector2.right };
    List<PlaneBase> m_targets = new List<PlaneBase>();

    private void Start()
    {
        //m_boxPlane = Physics2D.Raycast(transform.position, Vector2.zero)
    }

    private void OnMouseDown()
    {
        m_targets = new List<PlaneBase>();

        foreach (Vector2 dir in m_dirs)
            NotifyPlaneBaseByDir(dir);
    }

    void NotifyPlaneBaseByDir(Vector2 dir)
    {

        PlaneBase target = Physics2D.Raycast(transform.position, dir, 1f).collider.GetComponent<PlaneBase>();
        print(Physics2D.Raycast(transform.position, dir, 1f).collider.name);
        print("Notify");
        if (!target) return;

        target.OnBoxNotify(true);
        m_targets.Add(target);
    }

    void Update()
    {
        TouchInput();
        if (m_GameStateChannelSO.GameState != GameState.PLAY) return;

    }

    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
        print("Touch");
            Touch touch = Input.GetTouch(0);

            RaycastHit2D target = Physics2D.Raycast(touch.position, Vector2.zero, m_ObstacleLayerMask);

            if (!target || target.collider.name != gameObject.name) return;

            print("Touched");

            //if (touch.phase == TouchPhase.Began)
            //    startPos = touch.position;

            //if (touch.phase == TouchPhase.Moved)
            //    endPos = touch.position;

            //if (touch.phase == TouchPhase.Ended)
            //    if (DecideDirection() != Vector3.zero)
            //        playerController.SetDirection(DecideDirection());
        }
    }
}

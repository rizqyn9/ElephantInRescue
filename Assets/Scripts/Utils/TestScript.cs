using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 touchPos = Input.mousePosition;
        Transform camTrans = Camera.main.transform;
        //float dist = Vector3.Dot(camTrans.position, camTrans.forward);
        //touchPos.z = dist;
        Vector3 pos = Camera.main.ScreenToWorldPoint(touchPos);

        var a = Physics2D.RaycastAll(pos, Vector2.zero);

        foreach (var b in a)
        {
            print(b.collider.name);
        }

        //transform.position = new Vector2(pos.x, pos.y);
    }
}

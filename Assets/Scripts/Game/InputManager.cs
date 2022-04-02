using UnityEngine;
using EIR.Game;
using System.Collections;
using System;

public class InputManager : MonoBehaviour
{
    //small value to confirm its swipe or not
    [SerializeField] private float threshHold = 0.1f;

    //ref to playerController
    [SerializeField] private PlayerController playerController => PlayerController.Instance;

    //Vector3 to store start touch position and end touch position
    private Vector3 startPos, endPos;

    void Start()
    {
        startPos = endPos;
    }

    void Update()
    {
#if UNITY_EDITOR
        ListenWithKeys();
        MoveInput();
#elif UNITY_IOS || UNITY_ANDROID
        TouchInput();
#endif
    }

    /// <summary>
    /// Method called for mouse input
    /// </summary>
    ///

    [SerializeField] bool validClick = false;

    void MoveInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (!IsValidInput(Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 5f))) return;

                startPos = Input.mousePosition;

                validClick = true;
            }

            endPos = Input.mousePosition;
        }

        //when player lift the mouse button
        if (Input.GetMouseButtonUp(0) && validClick)
        {
            validClick = false;
            if (DecideDirection() != Vector3.zero)
                playerController.SetDirection(DecideDirection());
        }
    }

    /// <summary>
    /// Method used for touch input
    /// </summary>
    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (!IsValidInput(Physics2D.RaycastAll(touch.position, Vector2.zero))) return;

            if (touch.phase == TouchPhase.Began)
                startPos = touch.position;

            if (touch.phase == TouchPhase.Moved)
                endPos = touch.position;

            if (touch.phase == TouchPhase.Ended)
                if (DecideDirection() != Vector3.zero)
                    playerController.SetDirection(DecideDirection());
        }
    }

    /// <summary>
    /// Method to decide swipe direction
    /// </summary>
    /// <returns></returns>
    Vector3 DecideDirection()
    {
        Vector3 direction = Vector3.zero;

        if (Mathf.Abs(endPos.x - startPos.x) > Mathf.Abs(endPos.y - startPos.y))
        {
            if (Mathf.Abs(endPos.x - startPos.x) > threshHold)
            {
                if (endPos.x > startPos.x)
                {
                    direction = Vector3.right;
                }
                else if (endPos.x < startPos.x)
                {
                    direction = Vector3.left;
                }
            }
        }

        else if (Mathf.Abs(endPos.x - startPos.x) < Mathf.Abs(endPos.y - startPos.y))
        {
            if (Mathf.Abs(endPos.y - startPos.y) > threshHold)
            {
                if (endPos.y > startPos.y)
                {
                    direction = Vector3.up;
                }
                else if (endPos.y < startPos.y)
                {
                    direction = Vector3.down;
                }
            }
        }
        return direction;
    }

    void ListenWithKeys()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector3.right;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector3.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector3.left;

        if (direction == Vector3.zero) return;
        playerController.SetDirection(direction);
    }

    bool IsValidInput(RaycastHit2D[] rays)
    {
        if (rays.Length <= 0) return false;

        return Array.FindIndex(rays, res => res.transform.CompareTag("InputArea")) < 0 ? false : true;
    }
}
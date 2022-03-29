using UnityEngine;
using EIR.Game;

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
        MoveInput();
#elif UNITY_IOS || UNITY_ANDROID
        TouchInput();
#endif
    }

    /// <summary>
    /// Method called for mouse input
    /// </summary>
    void MoveInput()
    {
        //we check if left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            //if its down
            if (Input.GetMouseButtonDown(0))
            {
                //save the satrt position
                startPos = Input.mousePosition;
            }

            //as its pressed set endPos
            endPos = Input.mousePosition;
        }

        //when player lift the mouse button
        if (Input.GetMouseButtonUp(0))
        {
            //check for direction
            if (DecideDirection() != Vector3.zero)
            {
                //send direction to player
                playerController.SetDirection(DecideDirection());
            }
        }
    }

    /// <summary>
    /// Method used for touch input
    /// </summary>
    void TouchInput()
    {
        //if touch count is more than 0
        if (Input.touchCount > 0)
        {
            //get the 1st touch
            Touch touch = Input.GetTouch(0);

            //if touch.phase is begin
            if (touch.phase == TouchPhase.Began)
            {
                //save the startPos
                startPos = touch.position;
            }

            //if touch.phase is moved
            if (touch.phase == TouchPhase.Moved)
            {
                //save the endPos
                endPos = touch.position;
            }

            //if touch.phase is Ended
            if (touch.phase == TouchPhase.Ended)
            {
                //check for direction
                if (DecideDirection() != Vector3.zero)
                {
                    //send direction to player
                    playerController.SetDirection(DecideDirection());
                }
            }
        }
    }

    /// <summary>
    /// Method to decide swipe direction
    /// </summary>
    /// <returns></returns>
    Vector3 DecideDirection()
    {
        Vector3 direction = Vector3.zero;

        //if difference of X is more than Y
        if (Mathf.Abs(endPos.x - startPos.x) > Mathf.Abs(endPos.y - startPos.y))
        {
            //Swipe is on X Axis
            if (Mathf.Abs(endPos.x - startPos.x) > threshHold)
            {
                //right swipe
                if (endPos.x > startPos.x)
                {
                    direction = Vector3.right;
                }
                else if (endPos.x < startPos.x) //left swipe
                {
                    direction = Vector3.left;
                }
            }
        }
        //if difference of X is less than Y
        else if (Mathf.Abs(endPos.x - startPos.x) < Mathf.Abs(endPos.y - startPos.y))
        {
            //Swipe is on Y Axis
            if (Mathf.Abs(endPos.y - startPos.y) > threshHold)
            {
                //up swipe
                if (endPos.y > startPos.y)
                {
                    direction = Vector3.up;
                }
                else if (endPos.y < startPos.y) //down swipe
                {
                    direction = Vector3.down;
                }
            }
        }
        return direction;
    }
}
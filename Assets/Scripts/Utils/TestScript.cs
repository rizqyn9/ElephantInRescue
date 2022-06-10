using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool isKnocked = false;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ListenWithKeys();   
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

        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetBool("KnockedOut", true);


        if (direction == Vector3.zero || isKnocked) return;
        SetDirection(direction);
    }

    private void SetDirection(Vector3 direction)
    {
        if (isKnocked)
        {
            animator.SetBool("KnockedOut", true);
        } else
        {
            animator.SetBool("Walk", true);
        }

        animator.SetFloat("Y", direction.y);
        animator.SetFloat("X", direction.x);

        LeanTween.value(0, 1, 5f).setOnComplete(() =>
        {
            animator.SetBool("Walk", false);
        });
    }
}

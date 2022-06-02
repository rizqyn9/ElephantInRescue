using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
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

        if (direction == Vector3.zero) return;
        SetDirection(direction);
    }

    private void SetDirection(Vector3 direction)
    {
        animator.SetBool("Walk", true);
        if(direction.x > 0)
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);

        LeanTween.value(0, 1, 5f).setOnComplete(() =>
        {
            animator.SetBool("Walk", false);
        });
    }
}

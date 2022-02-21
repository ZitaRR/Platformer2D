using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Vector2 movement;
    private Animator animator;
    private new SpriteRenderer renderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Movement", Mathf.Abs(x));
        movement = new Vector2(x, 0);
        transform.position += (Vector3)movement.normalized * Time.deltaTime;
    }

    private void Rotation()
    {
        if (movement.x > 0)
            renderer.flipX = false;
        else if (movement.x < 0)
            renderer.flipX = true;
    }
}

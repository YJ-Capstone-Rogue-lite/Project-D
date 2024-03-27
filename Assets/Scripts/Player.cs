using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dashSpeed = 3f;


    Rigidbody2D rigid2d;

    Vector3 movement;
    bool isDashing = false;

    private void Start()
    {
        rigid2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButton("Dash"))
        {
            isDashing = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Dash();
    }


    void Move()
    {
        Vector3 moveVelocity= Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0 )
        {
            moveVelocity = Vector3.down;

        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.up;

        }

        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            moveVelocity = Vector3.right;

        }

        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            moveVelocity = Vector3.left;

        }

        transform.position += moveVelocity * moveSpeed * Time.deltaTime;

    }

    void Dash()
    {
        Debug.Log("Dash!");
        Debug.Log("Dash Cool Time Start");

        
    }

}

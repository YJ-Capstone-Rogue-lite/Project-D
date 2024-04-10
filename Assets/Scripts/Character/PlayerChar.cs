using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerChar : Character
{
    private Rigidbody2D player_Rb;
    private Animator player_anim;

    protected override void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
        player_anim = GetComponent<Animator>();

        base.Start();

    }
    private void FixedUpdate()
    {
        player_Rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * m_movementSpeed;

        if(player_Rb.velocity.x < 0 )
        {
            transform.localScale = new Vector3(-1f,1f, 1f);
        }
        else if(player_Rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            player_anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            player_anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
        player_anim.SetFloat("MoveX", player_Rb.velocity.x);
        player_anim.SetFloat("MoveY", player_Rb.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}

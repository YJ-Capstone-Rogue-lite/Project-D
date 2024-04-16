using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerChar : Character
{
    private Rigidbody2D player_Rb;
    private Animator player_anim;
    private float rollDuration = 0.7f; //구르는시간
    bool is_rolling = false;
    protected override void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
        player_anim = GetComponent<Animator>();

        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        player_Rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * m_movementSpeed;

        player_anim.SetFloat("MoveX", player_Rb.velocity.x);
        player_anim.SetFloat("MoveY", player_Rb.velocity.y);

        if (player_Rb.velocity.x < 0) //flip
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (player_Rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) //Idle
        {

            player_anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            player_anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
        if (!is_rolling)
        {
            player_Roll();
        }
        

    }
    public void player_Roll()
    {
        if (Input.GetButtonDown("Roll"))
        {
            Debug.Log("roll");
            player_anim.SetBool("isRolling", true);
            player_anim.SetFloat("RollX", Input.GetAxisRaw("Horizontal"));
            player_anim.SetFloat("RollY", Input.GetAxisRaw("Vertical"));
            is_rolling = true;

            StartCoroutine(EndRoll());
        }
    }
    IEnumerator EndRoll()
    {
        yield return new WaitForSeconds(rollDuration);
        player_anim.SetBool("isRolling", false);
        is_rolling = false;
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

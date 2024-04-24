using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChar : Character
{
    private Rigidbody2D player_Rb;
    private Animator player_anim;
    private float rollDuration = 0.7f; //구르는시간
    bool is_rolling = false;
    public GameObject camera_;
    private GameObject cameraInstance;
    //현재 주석처리 된 부분 사용시 플레이어 총기 스프라이트 위치가 미묘하게 달라지는 현상 있음
    //[SerializeField]private SpriteRenderer gunSpriteRenderer; // 총 스프라이트 렌더러

    protected override void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
        player_anim = GetComponent<Animator>();
        cameraInstance = Instantiate(camera_, transform.position, Quaternion.identity);
        //cameraInstance.SetActive(false);
        cameraInstance.GetComponent<Camera_Player>().player = gameObject;
        base.Start();

    }
    private void OnEnable()
    {
        //cameraInstance.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        player_Rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * m_movementSpeed;

        player_anim.SetFloat("MoveX", player_Rb.velocity.x);
        player_anim.SetFloat("MoveY", player_Rb.velocity.y);

        if (player_Rb.velocity.x < 0) //flip << 총 까지 같이 돌아가서 나중에 수정할것
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); //x y z
            //gunSpriteRenderer.transform.localScale = new Vector3(1f, -1f, 1f); //x y z



        }
        else if (player_Rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            //gunSpriteRenderer.transform.localScale = Vector3.one;

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

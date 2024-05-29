using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChar : Character
{
    public static PlayerChar single;
    public Fire_Test fire;
    private Rigidbody2D player_Rb;
    private Animator player_anim;
    private SpriteRenderer bodyRender;
    private int originalSortingOrder;
    private float rollDuration = 0.7f; //구르는시간
    bool is_rolling = false;
    public GameObject camera_;
    private GameObject cameraInstance;

    public Transform player_location;
    public Transform gun_rotation;


    //현재 주석처리 된 부분 사용시 플레이어 총기 스프라이트 위치가 미묘하게 달라지는 현상 있음
    //[SerializeField]private SpriteRenderer gunSpriteRenderer; // 총 스프라이트 렌더러

    private void Awake()
    {
        single = this;
    }

    protected override void Start()
    {
        
        player_Rb = GetComponent<Rigidbody2D>();
        player_anim = GetComponent<Animator>();
        bodyRender = GetComponent<SpriteRenderer>();
        cameraInstance = Instantiate(camera_, transform.position, Quaternion.identity);
        //cameraInstance.SetActive(false);
        cameraInstance.GetComponent<Camera_Player>().player = gameObject;
        //FloorLoader.Instance.player = gameObject;
        base.Start();
        originalSortingOrder = bodyRender.sortingOrder;


    }
    private void OnEnable()
    {
        //FloorLoader.Instance.player = gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPlaying)
        {
            return;
        }
        Debug.DrawRay(player_Rb.position, Vector2.down, new Color(1, 0, 0));
        RaycastHit2D [] hit = Physics2D.RaycastAll(player_Rb.position, Vector2.down, 1, LayerMask.GetMask("Wall"));
        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && !hit[i].collider.isTrigger)
            {
                // 레이캐스트가 "Wall" 레이어에 닿은 경우 "닿음" 메시지를 출력하고 sortingOrder를 0으로 설정합니다.
                Debug.Log("닿음");
                bodyRender.sortingOrder = 0;
            }
            // 레이캐스트가 아무 콜라이더에도 닿지 않은 경우
            else
            {
                // 원래 sortingOrder 값을 복원합니다.
                bodyRender.sortingOrder = originalSortingOrder;
            }
        }
        

        player_Rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * m_movementSpeed;
        var temp = (Vector3)fire.mousePos - transform.position;
        if (player_Rb.velocity.x != 0 || player_Rb.velocity.y != 0)
        {
            player_anim.SetFloat("MoveX", Mathf.Sign(temp.x));
            player_anim.SetFloat("MoveY", Mathf.Sign(temp.y));
        }
        else
        {
            player_anim.SetFloat("MoveX", 0);
            player_anim.SetFloat("MoveY", 0);
        }
        if (temp.x < 0 && !is_rolling) //마우스가 왼쪽으로 향할때 flip
        {
            // transform.localScale = new Vector3(-1f, 1f, 1f); //x y z
            //gunSpriteRenderer.transform.localScale = new Vector3(1f, -1f, 1f); //x y z

            bodyRender.flipX = true;
        }
        else if (temp.x > 0 && !is_rolling)
        {
            // transform.localScale = Vector3.one;
            //gunSpriteRenderer.transform.localScale = Vector3.one;
            bodyRender.flipX = false;
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
            if(player_Rb.velocity.x < 0)
            {
                bodyRender.flipX = true;
            }
            else if(player_Rb.velocity.x > 0)
            {
                bodyRender.flipX = false;
            }
            is_rolling = true;
            Debug.Log("roll");
            player_anim.SetBool("isRolling", true);
            player_anim.SetFloat("RollX", Input.GetAxisRaw("Horizontal"));
            player_anim.SetFloat("RollY", Input.GetAxisRaw("Vertical"));

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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerChar : Character
{
    public static PlayerChar single;

    public PlayerData playerdata;

    public Fire_Test fire;
    private Rigidbody2D player_Rb;
    public SpriteRenderer bodyRender;
    private int originalSortingOrder;
    private float rollDuration = 0.7f; //구르는시간
    private float stamina_recovery_value  = 25;//스태미나 재충전 값
    public GameObject camera_;
    private GameObject cameraInstance;

    public Transform player_location;
    public Transform gun_rotation;

    public SortingGroup SortingGroup_Weapon_slot1;
    public SortingGroup SortingGroup_Weapon_slot2;

    public Image stamina_bar; //플레이어 스태미나 바

    //현재 주석처리 된 부분 사용시 플레이어 총기 스프라이트 위치가 미묘하게 달라지는 현상 있음
    //[SerializeField]private SpriteRenderer gunSpriteRenderer; // 총 스프라이트 렌더러

    private void Awake()
    {
        if(single) Destroy(gameObject);
        single = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
        player_anim = GetComponent<Animator>();
        bodyRender = GetComponent<SpriteRenderer>();
        // cameraInstance = Instantiate(camera_, transform.position, Quaternion.identity);
        //cameraInstance.SetActive(false);
        // cameraInstance.GetComponent<Camera_Player>().player = gameObject;
        //FloorLoader.Instance.player = gameObject;
        base.Start();
        originalSortingOrder = bodyRender.sortingOrder;
        stamina_bar.gameObject.SetActive(false);
        Input.imeCompositionMode = IMECompositionMode.Auto;
        // charsingle = this;
        // DataManager를 통해 데이터 불러오기
        DataManager.Instance.LoadGameData();

        // DataManager에서 불러온 데이터를 playerdata에 할당

        // 플레이어 데이터가 제대로 할당되었는지 확인
        // Debug.Log("Player Max HP: " + playerdata.player_maxhp);
        // Debug.Log("Player Max Shield: " + playerdata.player_maxshield);
        // Debug.Log("Player Move Speed: " + playerdata.player_movespeed);
        // Debug.Log("Player Protection Time: " + playerdata.player_protectionTime);
        // Debug.Log(Player_Default_State);
        if(DataManager.Instance.data != null) playerdata = DataManager.Instance.data;
        if (playerdata == null) playerdata = new();

        // playerdata의 값을 해당 캐릭터로 연동하는 부분
        m_name = playerdata.player_name;
        m_maxHealth = playerdata.player_maxhp;
        m_maxShield = playerdata.player_maxshield;
        m_movementSpeed = playerdata.player_movespeed;
        m_max_movementSpeed = playerdata.player_max_movementSpeed;
        m_protectionTime = playerdata.player_protectionTime;
        m_maxStamina = playerdata.player_maxstamina;
        max_hp_UPStack = playerdata.player_max_hp_UPStack;
        movement_SpeedUpStack = playerdata.player_movement_SpeedUpStack;
        damageUpStack = playerdata.player_damageUpStack;
        Coin_Count = playerdata.player_Coin_Count;

        m_health = m_maxHealth;
        m_shield = m_maxShield;

        player_hpbar_update();
        player_shieldbar_update();



        // else
        // //아닐경우 플레이어 기본 스탯을 처음으로 로드
        // {
        //     m_health = Player_Default_State.Default_player_hp;
        //     m_maxHealth = Player_Default_State.Default_player_maxhp;
        //     m_shield = Player_Default_State.Default_player_shield;
        //     m_maxShield = Player_Default_State.Default_player_maxshield;
        //     m_movementSpeed = Player_Default_State.Default_player_movespeed;
        //     m_max_movementSpeed = Player_Default_State.Default_player_max_movementSpeed;
        //     m_protectionTime = Player_Default_State.Default_player_protectionTime;
        //     m_stamina = Player_Default_State.Default_player_stamina;
        //     m_maxStamina = Player_Default_State.Default_player_maxstamina;
        //     max_hp_UPStack = Player_Default_State.Default_player_max_hp_UPStack;
        //     movement_SpeedUpStack = Player_Default_State.Default_player_movement_SpeedUpStack;
        //     damageUpStack = Player_Default_State.Default_player_damageUpStack;
        //     Coin_Count = Player_Default_State.Default_player_Coin_Count;
        // }
    }
    private void OnEnable()
    {
        //FloorLoader.Instance.player = gameObject;
    }
    // Update is called once per frame
    void Update()
    {

        //스태미나 바 업데이트
        staminaba_update();

        if (!GameManager.Instance.isPlaying)
        {
            return;
        }
        Debug.DrawRay(player_Rb.position, Vector2.down, new Color(1, 0, 0));
        RaycastHit2D [] hit = Physics2D.RaycastAll(player_Rb.position, Vector2.down, 1, LayerMask.GetMask("Wall","Object"));
        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && !hit[i].collider.isTrigger)
            {
                if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    // 레이캐스트가 "Wall" 레이어에 닿은 경우
                    Debug.Log("Wall에 닿음");
                    bodyRender.sortingOrder = -1;
                    bodyRender.sortingLayerName = "Wall";
                }
                else if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Object"))
                {
                    // 레이캐스트가 "Object" 레이어에 닿은 경우
                    Debug.Log("Object에 닿음");
                    bodyRender.sortingOrder = -1;
                    bodyRender.sortingLayerName = "Object";
                }
            }
            else
            {
                // 레이캐스트가 아무 콜라이더에도 닿지 않은 경우
                bodyRender.sortingOrder = originalSortingOrder;
                bodyRender.sortingLayerName = "Player";
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
        fire = GameObject.FindWithTag("Weapon").GetComponent<Fire_Test>();


        //플레이어가 위를 보고있는지 확인해 무기 레이어 값 조절 하기
        if ((player_anim.GetFloat("MoveX") == 1) && (player_anim.GetFloat("MoveY") == 1) 
            || (player_anim.GetFloat("MoveX") == -1) && (player_anim.GetFloat("MoveY") == 1)
            ||(player_anim.GetFloat("LastMoveY") == 1))
        {
            SortingGroup_Weapon_slot1.enabled = false;
            SortingGroup_Weapon_slot2.enabled = false;

        }
        else
        {
            SortingGroup_Weapon_slot1.enabled = true;
            SortingGroup_Weapon_slot2.enabled = true;
        }

       


    }

  
    public void player_Roll()
    {
        if (Input.GetButtonDown("Roll") && m_stamina > 0)
        {
            if(player_Rb.velocity.x < 0)
            {
                bodyRender.flipX = true;
            }
            else if(player_Rb.velocity.x > 0)
            {
                bodyRender.flipX = false;
            }
            //플레이어가 구를때마다
            is_rolling = true;
            m_stamina -= 25; //현재 스태미나 값 -25 하고

            Debug.Log("구르기! " + "현재 스태미나 : " + m_stamina);
            player_anim.SetBool("isRolling", true);
            player_anim.SetFloat("RollX", Input.GetAxisRaw("Horizontal"));
            player_anim.SetFloat("RollY", Input.GetAxisRaw("Vertical"));
            // 기존에 실행 중인 InvokeRepeating을 취소한다.
            CancelInvoke("stamina_recovery");
            // 새로운 InvokeRepeating을 설정한다.
            InvokeRepeating("stamina_recovery", 2f, 1f); //스태미나 리커버리를, n초후에 n1초마다 불러옴
           
            StartCoroutine(EndRoll());
        }
        else if( m_stamina <=0)
        {
            Debug.Log("현재 스태미나가 " + m_stamina +" 값이라 구르기 불가");
        }
    }
    IEnumerator EndRoll()
    {
        yield return new WaitForSeconds(rollDuration);
        player_anim.SetBool("isRolling", false);
        is_rolling = false;
    }

    // 스태미나 회복 코드
    public void stamina_recovery()
    {
        if (m_stamina < m_maxStamina) // 현재 스태미나가 최대 스태미나보다 작을 때만 실행
        {
            m_stamina += stamina_recovery_value; // 스태미나를 회복 값만큼 더함
            Debug.Log("스태미나 회복 : " + m_stamina);

            if (m_stamina >= m_maxStamina) // 회복 후 스태미나가 최대치에 도달하면
            {
                Debug.Log("최대 스태미나에 도달함");
                CancelInvoke("stamina_recovery"); // 회복을 멈춤
            }
        }
    }

    //스태미나 바 업데이트
    public void staminaba_update()
    {
        if (m_stamina < m_maxStamina) //현재 스태미나가 최대 스태미나보다 작은 동안
        {
            stamina_bar.gameObject.SetActive(true); //스태미나 바 활성화
            stamina_bar.fillAmount = m_stamina/m_maxStamina;
        }
        else
        {
            stamina_bar.gameObject.SetActive(false); //아니면 비활성화
        }
    }

    private void player_die()
    {
        if (m_health <= 0 && m_shield <= 0) //플레이어 쉴드여부 관계없이 hp 0 되면 사망 수정해야함
        {
            player_anim.SetBool("State", false);
            SaveData();
            Debug.Log("플레이어 사망");
        }
        else Debug.Log(m_health + ", " + m_shield);
    }

    public void player_reset()
    {
        player_anim.SetBool("State", true);

        m_health = m_maxHealth;
        m_shield = m_maxShield;

        player_hpbar_update();
        player_shieldbar_update();
    }

    private void SaveData()
    {
        playerdata.player_name = m_name;
        playerdata.player_maxhp = m_maxHealth;
        playerdata.player_maxshield = m_maxShield;
        playerdata.player_movespeed = m_movementSpeed;
        playerdata.player_max_movementSpeed = m_max_movementSpeed;
        playerdata.player_protectionTime = m_protectionTime;
        playerdata.player_maxstamina = m_maxStamina;
        playerdata.player_max_hp_UPStack = max_hp_UPStack;
        playerdata.player_movement_SpeedUpStack = movement_SpeedUpStack;
        playerdata.player_damageUpStack = damageUpStack;
        playerdata.player_Coin_Count = Coin_Count;
        // DataManager.Instance.data = playerdata;
        DataManager.Instance.SaveGameData(playerdata);
    }

    //protected override void OnTriggerEnter2D(Collider2D collider)
    //{
    //    base.OnTriggerEnter2D(collider);
    //}
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        player_die();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        player_die();
    }

    void OnApplicationQuit() => SaveData();
    void OnDestroy() => SaveData();
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Character : MonoBehaviour
{
    private static Character charsingle;
    public static Character Charsingle
    {
        get
        {
            if (charsingle == null) charsingle = FindObjectOfType<Character>();
            // if (charsingle != null) charsingle = new Character();
            return charsingle;
        }
    }
    public bool player_state = true;
    [SerializeField] private GameObject hit_sound;

    // 캐릭터의 효과를 관리하는 컨트롤러
    [SerializeField]
    private EffectController effectController;
    [SerializeField] protected bool is_rolling = false;

    [SerializeField] private Weapon_Slot weapon_slot;

    // 캐릭터 상태 데이터
    [SerializeField]
    protected CharStateData charStateData;

    public GameObject HPbar;
    public GameObject Shield_bar;

    public GameObject hp_count;
    public GameObject shield_count;


    // 무적 상태 여부를 나타내는 변수
    public bool invincible = false;

    [Header("플레이어 스탯")]
    //캐릭터 닉네임
    public string m_name;

    // 캐릭터의 체력
    public float m_health;

    // 캐릭터 최대 체력
    public float m_maxHealth;

    // 캐릭터의 방패
    public float m_shield;

    //캐릭터의 최대 방패량
    public float m_maxShield;

    // 캐릭터의 현재 이동 속도(기본 3)
    public float m_movementSpeed;

    // 캐릭터의 버프로 증가되는 이동 속도
    public float m_buff_movementSpeed;

    // 캐릭터의 이동속도 최대치
    public float m_max_movementSpeed;

    // 캐릭터의 이동속도 최소치
    public float m_min_movementSpeed;

    // 캐릭터의 보호 시간
    public float m_protectionTime;

    // 캐릭터의 데미지(적에게가하는 데미지) 해당 데미지 공식은 enemy 코드에 있음.
    public float m_damage;

    // 캐릭터의 패시브 데미지(스택형 아이템을 먹을시 영구적으로 올라가는 데미지 값)
    public float m_passive_buff_damage;

    // 캐릭터의 버프로 인해 올라가는 데미지 값(지속시간 있는 버프)
    public float m_buff_damage;

    //캐릭터의 현재 스태미나(대쉬 관련)
    public float m_stamina;

    //스태미나 최대치(임시, 버프나 악세로 바뀜)
    public float m_maxStamina = 100;

    //플레이어가 획득한 코인
    public int Coin_Count;

    [Header("플레이어 버프 관련 스택")]
    // 스택을 저장할 변수
    public int damageUpStack = 0; //데미지업 스택

    public int movement_SpeedUpStack = 0; // 스피드업 스택

    public int max_hp_UPStack; // 최대 체력 스택



    public Animator player_anim;

    public Player_Default_State Player_Default_State = new Player_Default_State();

    //private void Awake()
    //{
    //    if (charsingle == null)
    //    {
    //        charsingle = this;
    //        DontDestroyOnLoad(gameObject);

    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }


    //}

    protected virtual void Start()
    {
        effectController = gameObject.AddComponent<EffectController>();


        Shield_bar = GameObject.Find("Shield_Bar_Img");
        HPbar = GameObject.Find("HP_Bar_Img");
        hp_count = GameObject.Find("hp_count");
        shield_count = GameObject.Find("shield_count");

        player_hpbar_update();
        player_shieldbar_update();
        m_stamina = m_maxStamina;//시작시 플레이어 스태미나 최대치로 조정
      
    }

  

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Hit_radius"))
        {
            // 충돌한 오브젝트가 데미지 오브젝트인 경우 피해를 입음
            Damaged(collider.GetComponent<DamageData>());
            return;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider;
        if (collider.CompareTag("Hit_radius"))
        {
            // 충돌한 오브젝트가 데미지 오브젝트인 경우 피해를 입음
            Damaged(collider.GetComponent<DamageData>());
            return;
        }
    }


    public virtual void Heal(DamageData damageData)
    {
        m_health += damageData.damage;
        if(m_health > m_maxHealth) m_health = m_maxHealth;
        if(damageData.effect != null) EffectAction((EffectData)damageData.effect);
        player_hpbar_update();
    }

    public virtual void Shield(DamageData damageData)
    {
        m_shield += damageData.damage;
        if(m_shield > m_maxShield) m_shield = m_maxShield;
        if(damageData.effect != null) EffectAction((EffectData)damageData.effect);
        player_shieldbar_update();
    }

    public virtual void Damaged(DamageData damageData)
    {
        // 무적 상태인 경우 피해를 받지 않음
        if (invincible || is_rolling)
            return;

        if (m_shield > 0) // 쉴드가 있을 때
        {
            m_shield -= damageData.damage; // 쉴드에서 피해 감소

           
            if (m_shield <= 0) // 쉴드가 음수가 되지 않도록 보정
            {
                m_shield = 0;
                // 쉴드가 아직 남아있는 경우에만 쉴드 바 업데이트
                player_shieldbar_update();
            }
            // 쉴드가 아직 남아있는 경우에만 쉴드 바 업데이트
            player_shieldbar_update();
        }
        else // 쉴드가 없을 때
        {
            m_health -= damageData.damage; // 체력에서 피해 감소
                                           // 체력이 아직 남아있는 경우에만 체력 바 업데이트
            player_hpbar_update();
            if (m_health <= 0) // 체력이 음수가 되지 않도록 보정
            {
                m_health = 0;
                player_hpbar_update();

            }

        }

        if(damageData.effect != null) EffectAction((EffectData)damageData.effect);

        // 피격 후 무적 상태로 변경
        StartCoroutine(InvincibleCoroutine());
    }

    public void EffectAction(EffectData effect)
    {
        var temp = gameObject.AddComponent<Effect>();
        temp.m_effectData = effect;
        effectController.Operation(this, temp); // 효과가 있는 경우 효과 적용
    }

    public bool GetPlayerState()
    {
        return player_state;
    }

    IEnumerator InvincibleCoroutine()
    {
        Debug.Log("피격 후 무적");
        // 무적 상태로 설정
        invincible = true;
        StartCoroutine(playerBlink());
        yield return new WaitForSeconds(m_protectionTime); // 일정 시간 동안 대기
        // 무적 상태 해제
        invincible = false;
        hit_sound.SetActive(false);
    }

    IEnumerator playerBlink()
    {
        if (invincible)
        {
            hit_sound.SetActive(true);
            // 깜빡이는 효과를 위해 while 루프 사용
            float blinkInterval = 0.2f; // 각 깜빡이는 간격

            while (invincible)
            {
                // 플레이어의 색상을 변경하여 깜빡이는 효과 생성
                PlayerChar.single.bodyRender.material.color = new Color32(255, 255, 255, 180);
                yield return new WaitForSeconds(blinkInterval);

                PlayerChar.single.bodyRender.material.color = Color.white; // 기본 색상으로 돌아감
                yield return new WaitForSeconds(blinkInterval);
            }
        }
    }
    private void playerDie_State() // Die애니메이션 끝에 실행되는 메소드
    {
        player_state = false;
    }



    public void player_shieldbar_update() // 쉴드 바 업데이트
    {
        Image ShieldBarImage = Shield_bar.GetComponent<Image>();
        ShieldBarImage.fillAmount = m_shield / m_maxShield; // 쉴드 비율로 fillAmount 설정
        
        TMP_Text shield_count_text = shield_count.GetComponent<TMP_Text>();
        shield_count_text.text = m_shield.ToString();

    }

    public void player_hpbar_update() // 체력 바 업데이트
    {
        Image HPbarImage = HPbar.GetComponent<Image>();
        HPbarImage.fillAmount = m_health / m_maxHealth; // 체력 비율로 fillAmount 설정

        TMP_Text hp_count_text = hp_count.GetComponent<TMP_Text>();
        hp_count_text.text = m_health.ToString() +"/" + m_maxHealth;

    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public static Character charsingle;
    private bool player_state = true;
    [SerializeField] private GameObject hit_sound;

    // 캐릭터의 효과를 관리하는 컨트롤러
    [SerializeField]
    private EffectController effectController;
    [SerializeField] protected bool is_rolling = false;

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
    public string m_name;

    // 캐릭터의 체력
    public float m_health;

    // 캐릭터 최대 체력
    public float m_maxHealth;

    // 캐릭터의 방패
    public float m_shield;

    //캐릭터의 최대 방패량
    public float m_maxShield;

    // 캐릭터의 이동 속도
    public float m_movementSpeed;

    // 캐릭터의 보호 시간
    public float m_protectionTime;

    // 캐릭터의 자체 데미지(버프나 악세사리 스탯 증감용)
    public float m_damage;

    //캐릭터의 현재 스태미나(대쉬 관련)
    public float m_stamina;

    //스태미나 최대치(임시)
    public float m_maxStamina = 100;

    public Animator player_anim;

    public PlayerData playerdata = new PlayerData();

    protected virtual void Start()
    {
        // DataManager를 통해 데이터 불러오기
        DataManager.Instance.LoadGameData();

        // DataManager에서 불러온 데이터를 playerdata에 할당
        playerdata = DataManager.Instance.data;

        effectController = gameObject.AddComponent<EffectController>();

        // 플레이어 데이터가 제대로 할당되었는지 확인
        Debug.Log("Player Max HP: " + playerdata.player_maxhp);
        Debug.Log("Player HP: " + playerdata.player_hp);
        Debug.Log("Player Max Shield: " + playerdata.player_maxshield);
        Debug.Log("Player Shield: " + playerdata.player_shield);
        Debug.Log("Player Move Speed: " + playerdata.player_movespeed);
        Debug.Log("Player Protection Time: " + playerdata.player_protectionTime);

        // 캐릭터의 상태 데이터로 초기화
        m_name = playerdata.player_name;
        m_health = playerdata.player_hp;
        m_maxHealth = playerdata.player_maxhp;
        m_shield = playerdata.player_shield;
        m_maxShield = playerdata.player_maxshield;
        m_movementSpeed = playerdata.player_movespeed;
        m_protectionTime = playerdata.player_protectionTime;
        m_stamina = playerdata.player_stamina;
        m_maxStamina = playerdata.player_maxstamina;
        m_damage = charStateData.player_damage;
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
        if (collider.CompareTag("DamageObject") || collider.CompareTag("Hit_radius"))
        {
            // 충돌한 오브젝트가 데미지 오브젝트인 경우 피해를 입음
            Damaged(collider.GetComponent<DamageData>());
            return;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider;
        if (collider.CompareTag("DamageObject") || collider.CompareTag("Hit_radius"))
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

        player_die();
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



    protected void player_die()
    {
        if (m_health <= 0 && m_shield <= 0) //플레이어 쉴드여부 관계없이 hp 0 되면 사망 수정해야함
        {
            player_anim.SetBool("State", false);
            m_movementSpeed = 0;
            Debug.Log("플레이어 사망");
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
        hp_count_text.text = m_health.ToString();

    }
}



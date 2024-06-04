using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public static Character charsingle;
    private bool player_state = true;
    // 캐릭터의 효과를 관리하는 컨트롤러
    [SerializeField]
    private EffectController effectController;

    // 캐릭터 상태 데이터
    [SerializeField]
    protected CharStateData charStateData;
    public GameObject HPbar;
    public GameObject Shield_bar;

    // 무적 상태 여부를 나타내는 변수
    private bool invincible = false;

    // 무적 지속 시간
    private float invincibleDuration = 0.2f;

    [Header("플레이어 스탯")]
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

    public Animator player_anim;
    protected virtual void Start()
    {
        // 캐릭터의 상태 데이터로 초기화
        m_health = charStateData.health;
        m_maxHealth = 100;
        m_shield = charStateData.shield;
        m_maxShield = charStateData.max_shield;
        m_movementSpeed = 5;
        m_protectionTime = 0;
        m_damage = charStateData.player_damage;
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


    protected virtual void Damaged(DamageData damageData)
    {
        // 무적 상태인 경우 피해를 받지 않음
        if (invincible)
            return;

        if (m_shield > 0) // 쉴드가 있을 때
        {
            m_shield -= damageData.damage; // 쉴드에서 피해 감소

            // 쉴드가 아직 남아있는 경우에만 쉴드 바 업데이트
            player_shieldbar_update();
            if (m_shield <= 0) // 쉴드가 음수가 되지 않도록 보정
            {
                m_shield = 0;
            }
        }
        else // 쉴드가 없을 때
        {
            m_health -= damageData.damage; // 체력에서 피해 감소
                                           // 체력이 아직 남아있는 경우에만 체력 바 업데이트
            player_hpbar_update();
            if (m_health <= 0) // 체력이 음수가 되지 않도록 보정
            {
                m_health = 0;
            }
          
        }

        if (effectController != null)
            effectController.Operation(damageData.effect); // 효과가 있는 경우 효과 적용
        Debug.Log(damageData.damage + "현재 남은 쉴드 " + m_shield); // 디버그 로그 출력
        Debug.Log(damageData.damage + "현재 남은 체력 " + m_health); // 디버그 로그 출력

        // 피격 후 무적 상태로 변경
        StartCoroutine(InvincibleCoroutine());

        player_die();
    }

    IEnumerator InvincibleCoroutine()
    {
        Debug.Log("피격 후 무적");
        // 무적 상태로 설정
        invincible = true;
        yield return new WaitForSeconds(invincibleDuration); // 일정 시간 동안 대기
        // 무적 상태 해제
        invincible = false;
    }
    protected void player_die()
    {
        if (m_health <= 0 && m_shield <= 0) //플레이어 쉴드여부 관계없이 hp 0 되면 사망 수정해야함
        {
            player_anim.SetBool("State", false);
            Debug.Log("플레이어 사망");
        }
    }
    private void playerDie_State() // Die애니메이션 끝에 실행되는 메소드
    {
        player_state = false;
    }



    public void player_shieldbar_update() // 쉴드 바 업데이트
    {
        Shield_bar = GameObject.Find("Shield_Bar_Img");
        Image ShieldBarImage = Shield_bar.GetComponent<Image>();
        ShieldBarImage.fillAmount = m_shield / m_maxShield; // 쉴드 비율로 fillAmount 설정
    }

    public void player_hpbar_update() // 체력 바 업데이트
    {
        HPbar = GameObject.Find("HP_Bar_Img");
        Image HPbarImage = HPbar.GetComponent<Image>();
        HPbarImage.fillAmount = m_health / m_maxHealth; // 체력 비율로 fillAmount 설정

    }

}



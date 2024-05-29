using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 캐릭터의 효과를 관리하는 컨트롤러
    [SerializeField]
    private EffectController effectController;

    // 캐릭터 상태 데이터
    [SerializeField]
    protected CharStateData charStateData;

    [Header("플레이어 스탯")]
    // 캐릭터의 체력
    protected float m_health;

    // 캐릭터의 방패
    protected float m_shield;

    // 캐릭터의 이동 속도
    protected float m_movementSpeed;

    // 캐릭터의 보호 시간
    protected float m_protectionTime;

    // 캐릭터의 자체 데미지(버프나 악세사리 스탯 증감용)
    protected float m_damage;

    protected virtual void Start()
    {
        // 캐릭터의 상태 데이터로 초기화
        m_health = charStateData.health;
        m_shield = charStateData.shield;
        m_movementSpeed = 5;
        m_protectionTime = 0;
        m_damage = charStateData.player_damage;
    }

    protected virtual void Damaged(DamageData damageData)
    {
        // 캐릭터가 피해를 받았을 때 호출되는 메서드
        m_shield -= damageData.damage; // 방패에서 피해 감소
        m_health -= damageData.damage; // 체력에서 피해 감소
        if (m_shield <= 0) m_shield = 0; // 방패가 음수가 되지 않도록 보정
        if (effectController != null) effectController.Operation(damageData.effect); // 효과가 있는 경우 효과 적용
        Debug.Log(damageData.damage); // 디버그 로그 출력
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("DamageObject") || collider.CompareTag("Enemy"))
        {
            // 충돌한 오브젝트가 데미지 오브젝트인 경우 피해를 입음
            Damaged(collider.GetComponent<DamageData>());
            return;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider;
        if (collider.CompareTag("DamageObject") || collider.CompareTag("Enemy"))
        {
            // 충돌한 오브젝트가 데미지 오브젝트인 경우 피해를 입음
            Damaged(collider.GetComponent<DamageData>());
            return;
        }
    }
}

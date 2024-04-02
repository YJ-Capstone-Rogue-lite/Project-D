using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 이펙트 컨트롤러
    [SerializeField]
    private EffectController effectController;

    // 캐릭터 상태 데이터
    [SerializeField]
    protected CharStateData charStateData;


    // 체력
    protected float m_health;

    // 보호막
    protected float m_sheild;

    // 이동 속도
    protected float m_movementSpeed;

    // 보호 시간
    protected float m_protectionTime;

    // 시작 함수
    protected virtual void Start()
    {
        // 이펙트 컨트롤러 초기화
        effectController = new EffectController();

        // 캐릭터 상태 데이터 초기화
        // charStateData = new CharStateData();

        // 체력 초기화
        m_health = charStateData.health;

        // 보호막 초기화
        m_health = charStateData.shield;

        // 이동 속도 초기화
        m_movementSpeed = 0;

        // 보호 시간 초기화
        m_protectionTime = 0;
    }

    // 데미지를 입었을 때 호출되는 함수
    protected virtual void Damaged(DamageData damageData)
    {
        // 보호막 감소
        m_sheild -= damageData.damage;

        // 체력 감소
        m_health -= m_sheild < 0 ? -m_sheild : 0;

        Debug.Log("데미지 " + damageData.damage + "입음");
    }

    // 트리거 충돌이 발생했을 때 호출되는 함수
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알, 적(과 충돌했을 때
        if (collision.CompareTag("Bullet") || (collision.CompareTag("Enemy")))
        {
            // 데미지 입히기
            Damaged(collision.GetComponent<DamageData>());
            return;

        }

        // 아이템과 충돌했을 때
        if (collision.CompareTag("Item"))
            Debug.Log("아이템과 충돌");
        {
            var item = collision.GetComponent<Item>();

            // 장착 가능한 아이템인 경우
            if (item is IEquipableItem<Effect>)
            {
                // 이펙트 획득 및 처리
                effectController.Operation(((IEquipableItem<Effect>)item).Acquir());
            }
            else
            {
                // 아이템 획득
                ((IAcquisableItem)item).Acquir();
            }
            return;
        }
    }
}
